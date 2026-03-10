using System.Security.Claims;

namespace Backend.Identity.Application.Common.Abstractions;

public interface ITokenService
{
    string GetAccessToken(Guid id, string login, string role);
    string GetRefreshToken(Guid id, string login, string role);
    Task<ClaimsPrincipal?> ValidateRefreshTokenAsync(string token, CancellationToken ct = default);
}