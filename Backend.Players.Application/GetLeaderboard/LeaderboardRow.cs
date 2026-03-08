namespace Backend.Players.Application.GetLeaderboard;

public sealed record LeaderboardRow(int Position, string Nickname, int Rating);