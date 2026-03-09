using System.Text.Json.Serialization;
using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.Login;

public sealed record LoginCommand(
    string Login,
    string Password
) : ICommand<LoginResult>;