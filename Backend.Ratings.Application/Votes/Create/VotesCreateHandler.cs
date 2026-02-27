using Backend.Ratings.Application.Common.Abstractions;
using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Domain.Votes;
using Backend.Shared.Exceptions;
using Backend.Shared.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backend.Ratings.Application.Votes.Create;

public class VotesCreateHandler(
    IVoteRepository voteRepository,
    IRatingReasonRepository ratingReasonRepository,
    IPlayerClient playerClient,
    ICurrentPlayer currentPlayer,
    IUnitOfWork unitOfWork
) : IRequestHandler<VotesCreateCommand, VotesCreateResult>
{
    public async Task<VotesCreateResult> Handle(VotesCreateCommand request, CancellationToken cancellationToken)
    {
        var player = await playerClient.EnsurePlayerAsync(request.Nickname, cancellationToken);

        if (!await ratingReasonRepository.ExistsAndActiveById(request.ReasonId, cancellationToken))
        {
            throw new NotFoundAppException($"Reason with ID=({request.ReasonId}) not found");
        }

        var vote = new Vote(player.Id, currentPlayer.Id, request.ReasonId, request.Comment);
        try
        {
            await voteRepository.CreateAsync(vote, cancellationToken);
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