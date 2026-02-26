using System.Text.Json.Serialization;

namespace Backend.Identity.Application.Register;

public sealed record RegisterResult(
    long Id,
    string Login,
    string Role
);