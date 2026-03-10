using System.Text.Json.Serialization;

namespace Backend.Identity.Application.Login;

public sealed record LoginResult(
    Guid Id,
    string Login,
    string Role
);