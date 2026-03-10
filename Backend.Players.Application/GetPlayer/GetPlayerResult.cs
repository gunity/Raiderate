namespace Backend.Players.Application.GetPlayer;

public sealed record GetPlayerResult(Guid Id, string Nickname, int Rating, int VotesCount);