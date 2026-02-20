using System.Text.Json.Serialization;
using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.Login;

public sealed record LoginCommand(
    [property: JsonPropertyName("login")]
    string Login,
    [property: JsonPropertyName("password")]
    string Password
) : ICommand<LoginResult>;