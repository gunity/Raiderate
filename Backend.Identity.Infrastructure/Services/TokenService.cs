using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Identity.Application.Common.Abstractions;
using Backend.Shared.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Identity.Infrastructure.Services;

public sealed class TokenService(IOptions<JwtOptions> jwtOptions) : ITokenService
{
    private static readonly JwtSecurityTokenHandler Handler = new();

    private readonly TokenValidationParameters _tokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Value.Issuer,

        ValidateAudience = true,
        ValidAudience = jwtOptions.Value.Audience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),

        ValidateLifetime = true,
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };
    
    public string GetAccessToken(Guid id, string login, string role)
    {
        return IssueToken(id, login, role, DateTime.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenExpireMinutes));
    }

    public string GetRefreshToken(Guid id, string login, string role)
    {
        return IssueToken(id, login, role, DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpireDays));
    }

    public async Task<ClaimsPrincipal?> ValidateRefreshTokenAsync(string token, CancellationToken ct = default)
    {
        var result = await Handler.ValidateTokenAsync(token, _tokenValidationParameters);

        if (!result.IsValid || result.ClaimsIdentity is null)
        {
            return null;
        } 
        
        return new ClaimsPrincipal(result.ClaimsIdentity);
    }

    private string IssueToken(Guid id, string login, string role, DateTime expires)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, role)
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}