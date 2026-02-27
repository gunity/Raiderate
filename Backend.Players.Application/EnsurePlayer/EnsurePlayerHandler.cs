using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Players.Domain.Players;
using MediatR;

namespace Backend.Players.Application.EnsurePlayer;

public class EnsurePlayerHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<EnsurePlayerCommand, EnsurePlayerResult>
{
    public async Task<EnsurePlayerResult> Handle(EnsurePlayerCommand request, CancellationToken ct)
    {
        var player = await playerRepository.GetReadonlyByNicknameAsync(request.Nickname, ct);
        if (player is null)
        {
            player = new Player(request.Nickname);
            await playerRepository.AddAsync(player, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
        
        return new EnsurePlayerResult(player.Id, player.Nickname);
    }
}