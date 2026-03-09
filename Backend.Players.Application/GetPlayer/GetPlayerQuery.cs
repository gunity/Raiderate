using Backend.Players.Application.Common.Abstractions.Messaging;

namespace Backend.Players.Application.GetPlayer;

public sealed record GetPlayerQuery(string Nickname) : IQuery<GetPlayerResult>;
