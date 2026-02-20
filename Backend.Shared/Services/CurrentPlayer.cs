using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Shared.Services;

public class CurrentPlayer(
    IHttpContextAccessor contextAccessor
) : ICurrentPlayer
{
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public long Id
    {
        get
        {
            var rawId = GetClaimValue(ClaimTypes.NameIdentifier);
            if (!long.TryParse(rawId, out var id))
            {
                throw new Exception($"Invalid {nameof(Id)}, check {nameof(IsAuthenticated)} first");
            }

            return id;
        }
    }

    public string Login
    {
        get
        {
            var rawLogin = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(rawLogin))
            {
                throw new Exception($"Invalid {nameof(Login)}, check {nameof(IsAuthenticated)} first");
            }

            var login = rawLogin.Trim();
            return login;
        }
    }

    public string Role
    {
        get
        {
            var rawRole = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(rawRole))
            {
                throw new Exception($"Invalid {nameof(Role)}, check {nameof(IsAuthenticated)} first");
            }
            
            var role = rawRole.Trim();
            return role;
        }
    }

    private string? GetClaimValue(string type) 
        => User?.Claims.FirstOrDefault(x => x.Type == type)?.Value;

    private ClaimsPrincipal? User 
        => contextAccessor.HttpContext?.User;
}