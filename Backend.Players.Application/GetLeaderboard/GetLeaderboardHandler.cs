using Backend.Players.Application.Common.Abstractions.Persistence;
using MediatR;

namespace Backend.Players.Application.GetLeaderboard;

public class GetLeaderboardHandler(IPlayerRepository playerRepository) : IRequestHandler<GetLeaderboardQuery, GetLeaderboardResult>
{
    public async Task<GetLeaderboardResult> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var limit = request.Limit;

        var items = request.Type switch
        {
            LeaderboardType.Top => await playerRepository.GetReadonlyTopAsync(limit, cancellationToken),
            LeaderboardType.Bottom => await playerRepository.GetReadonlyBottomAsync(limit, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Type))
        };

        var rows = items
            .Select((x, position) => new LeaderboardRow(position + 1, x.Nickname, x.Rating))
            .ToList();
        
        return new GetLeaderboardResult(rows);
    }
}