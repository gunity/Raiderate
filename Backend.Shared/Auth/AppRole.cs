namespace Backend.Shared.Auth;

public enum AppRole
{
    User,
    Admin
}

public static class AppRoleWire
{
    public const string User = "user";
    public const string Admin = "admin";
}