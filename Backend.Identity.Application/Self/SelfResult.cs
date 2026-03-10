namespace Backend.Identity.Application.Self;

public sealed record SelfResult(
    Guid Id,
    string Login,
    string Role
);