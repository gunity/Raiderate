using Backend.Players.Application.Common.Abstractions.Messaging;

namespace Backend.Players.Application.GetLeaderboard;

public sealed record GetLeaderboardQuery(LeaderboardType Type, int Limit) : IQuery<GetLeaderboardResult>;