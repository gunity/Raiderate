using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Players.Application.ApplyVote;

public class ApplyVoteHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<ApplyVoteCommand, Unit>
{
    public async Task<Unit> Handle(ApplyVoteCommand request, CancellationToken cancellationToken)
    {
        var playerId =  request.PlayerId;
        var delta = request.Delta;

        var player = await playerRepository.GetByIdAsync(playerId, cancellationToken);
        if (player is null)
        {
            throw new NotFoundAppException($"Player with ID=({playerId}) not found");
        }

        player.ApplyVote(delta);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}