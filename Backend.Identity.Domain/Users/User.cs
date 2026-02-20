
using Backend.Shared.Auth;

namespace Backend.Identity.Domain.Users;

public class User
{
    public long Id { get; private set; } 
    public string Login { get;  private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public AppRole Role { get; private set; }

    public User() { } // EF

    public User(string login, string passwordHash, AppRole role = AppRole.User)
    {
        Login = NormalizeLogin(login);
        PasswordHash = passwordHash;
        Role = role;
    }

    private static string NormalizeLogin(string login) => login.ToLowerInvariant().Trim();
}