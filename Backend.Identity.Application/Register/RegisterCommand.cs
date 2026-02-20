using System.Text.Json.Serialization;
using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.Register;

public sealed record RegisterCommand(
    [property: JsonPropertyName("login")]
    string Login,
    [property: JsonPropertyName("password")]
    string Password
) : ICommand<RegisterResult>;