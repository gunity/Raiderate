using Backend.Contracts.Events;
using Backend.Ratings.Application.Common.Abstractions;
using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Domain.Votes;
using Backend.Shared.Exceptions;
using Backend.Shared.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backend.Ratings.Application.Votes.Create;

public class VotesCreateHandler(
    IVoteRepository voteRepository,
    IRatingReasonRepository ratingReasonRepository,
    IPlayerClient playerClient,
    ICurrentPlayer currentPlayer,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint
) : IRequestHandler<VotesCreateCommand, VotesCreateResult>
{
    public async Task<VotesCreateResult> Handle(VotesCreateCommand request, CancellationToken cancellationToken)
    {
        var player = await playerClient.EnsurePlayerAsync(request.Nickname, cancellationToken);

        var reason = await ratingReasonRepository.GetActiveReadonlyAsync(request.ReasonId, cancellationToken);
        if (reason is null)
        {
            throw new NotFoundAppException($"Reason with ID=({request.ReasonId}) not found");
        }

        var playerId = Guid.Parse(player.Id);
        var vote = new Vote(playerId, currentPlayer.Id, request.ReasonId, request.Comment);
        try
        {
            await voteRepository.CreateAsync(vote, cancellationToken);
            
            var voteCreated = new VoteCreated(playerId, reason.Value, currentPlayer.Id, reason.Id);
            await publishEndpoint.Publish(voteCreated, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception) 
            when (exception.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            throw new AlreadyExistsAppException("Vote already exists");
        }
        
        return new VotesCreateResult(vote.Id, vote.PlayerId);
    }
}