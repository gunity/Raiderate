using Backend.Players.Application.Common.Abstractions.Messaging;

namespace Backend.Players.Application.EnsurePlayer;

public sealed record EnsurePlayerCommand(string Nickname) : ICommand<EnsurePlayerResult>;