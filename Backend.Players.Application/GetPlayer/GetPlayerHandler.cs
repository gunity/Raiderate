using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Players.Application.GetPlayer;

public class GetPlayerHandler(
    IPlayerRepository playerRepository
) : IRequestHandler<GetPlayerQuery, GetPlayerResult>
{
    public async Task<GetPlayerResult> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var nickname = request.Nickname.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(nickname))
        {
            throw new NotFoundException($"Player with nickname=({request.Nickname}) not found");
        }
        var player = await playerRepository.GetReadonlyByNicknameAsync(request.Nickname, cancellationToken);
        if (player is null)
        {
            throw new NotFoundException($"Player with nickname=({request.Nickname}) not found");
        }

        return new GetPlayerResult(player.Id, player.Nickname, player.Rating, player.VotesCount);
    }
}