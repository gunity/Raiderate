using Backend.Players.Application.Common.Abstractions.Messaging;

namespace Backend.Players.Application.GetOrCreate;

public sealed record GetOrCreatePlayerCommand(string Nickname) : ICommand<GetOrCreatePlayerResult>;