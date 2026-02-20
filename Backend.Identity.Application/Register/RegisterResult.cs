using System.Text.Json.Serialization;

namespace Backend.Identity.Application.Register;

public sealed record RegisterResult(
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonPropertyName("login")]
    string Login,
    [property: JsonPropertyName("role")]
    string Role
);