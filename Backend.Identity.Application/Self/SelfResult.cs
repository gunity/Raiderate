namespace Backend.Identity.Application.Self;

public sealed record SelfResult(
    long Id,
    string Login,
    string Role
);