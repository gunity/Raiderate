using System.Security.Claims;
using System.Text.Encodings.Web;
using Backend.Shared.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Backend.Shared.Auth;

internal class AuthenticationHandler(
    IOptionsMonitor<AuthenticationOptions> options,
    IOptions<JwtOptions> jwtOptions,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!TryValidate(AuthHeaders.SecretKey, out var secret) || jwtOptions.Value.Key != secret)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!TryValidate(AuthHeaders.UserId, out var id))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
        if (!TryValidate(AuthHeaders.UserLogin, out var login))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
        if (!TryValidate(AuthHeaders.UserRole, out var role))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, AuthenticationOptions.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationOptions.Scheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));

        bool TryValidate(string key, out string value)
        {
            value = "";
            if (!Request.Headers.TryGetValue(key, out var stringValues))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(stringValues.ToString()))
            {
                return false;
            }
            value = stringValues.ToString();
            return true;
        }
    }
}