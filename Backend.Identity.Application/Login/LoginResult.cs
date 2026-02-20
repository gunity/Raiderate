using System.Text.Json.Serialization;

namespace Backend.Identity.Application.Login;

public sealed record LoginResult(
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonPropertyName("login")]
    string Login,
    [property: JsonPropertyName("role")]
    string Role
);