using System.Text.Json.Serialization;

namespace Backend.Identity.Application.Register;

public sealed record RegisterResult(
    Guid Id,
    string Login,
    string Role
);