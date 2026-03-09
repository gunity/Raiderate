namespace Backend.Players.Application.GetLeaderboard;

public sealed record GetLeaderboardResult(IReadOnlyList<LeaderboardRow> Items);