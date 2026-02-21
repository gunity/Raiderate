namespace Backend.Players.Application.GetPlayer;

public sealed record GetPlayerResult(long Id, string Nickname, int Rating, int VotesCount);