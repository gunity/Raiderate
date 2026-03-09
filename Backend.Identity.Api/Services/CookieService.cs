using Backend.Shared.Options;
using Microsoft.Extensions.Options;

namespace Backend.Identity.Api.Services;

public class CookieService(IOptions<JwtOptions> jwtOptions) : ICookieService
{
    private readonly CookieOptions _cookieOptions = new()
    {
        HttpOnly = true
    };
    
    public void WriteCookie(IResponseCookies cookies, string accessToken, string refreshToken)
    {
        cookies.Append(jwtOptions.Value.AccessTokenName, accessToken, _cookieOptions);
        cookies.Append(jwtOptions.Value.RefreshTokenName, refreshToken, _cookieOptions);
    }
    
    public void DeleteCookie(IResponseCookies cookies)
    {
        cookies.Delete(jwtOptions.Value.AccessTokenName, _cookieOptions);
        cookies.Delete(jwtOptions.Value.RefreshTokenName, _cookieOptions);
    }

    public bool TryGetRefreshToken(IRequestCookieCollection cookies, out string token)
    {
        return cookies.TryGetValue(jwtOptions.Value.RefreshTokenName, out token!);
    }
}