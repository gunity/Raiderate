namespace Backend.Shared.Auth;

public static class AppRoleMapper
{
    public static string ToWrite(AppRole role)
    {
        return role.ToString().ToLowerInvariant();
    }

    public static AppRole Parse(string? value)
    {
        if (Enum.TryParse(value, ignoreCase: true, out AppRole role))
        {
            return role;
        }
        throw new Exception("Unknown role");
    }
}