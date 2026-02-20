using System.Text.Json.Serialization;

namespace Backend.Identity.Application.Self;

public sealed record SelfResult(
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonPropertyName("login")]
    string Login,
    [property: JsonPropertyName("role")]
    string Role
);