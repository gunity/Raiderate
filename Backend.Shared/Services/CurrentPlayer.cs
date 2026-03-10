using System.Security.Claims;
using Backend.Shared.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Shared.Services;

public class CurrentPlayer(
    IHttpContextAccessor contextAccessor
) : ICurrentPlayer
{
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public Guid Id
    {
        get
        {
            var rawId = GetClaimValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(rawId, out var id))
            {
                throw new UnauthorizedAppException("Invalid ID");
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
                throw new UnauthorizedAppException($"Invalid {nameof(Login)}");
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
                throw new UnauthorizedAppException($"Invalid {nameof(Role)}");
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