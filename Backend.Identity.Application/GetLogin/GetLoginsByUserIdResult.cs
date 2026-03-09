namespace Backend.Identity.Application.GetLogin;

public sealed record GetLoginsByUserIdResult(GetLoginsByUserIdItem[] Items);

public sealed record GetLoginsByUserIdItem(long Id, string Login);