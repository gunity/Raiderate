using Backend.Shared.Options;
using Microsoft.Extensions.Options;

namespace Backend.Identity.Api.Services;

public class CookieService(IOptions<JwtOptions> jwtOptions) : ICookieService
{
    private readonly CookieOptions _cookieOptions = new CookieOptions
    {
        HttpOnly = true
    };
    
    public void WriteCookie(IResponseCookies cookies, string token)
    {
        cookies.Append(jwtOptions.Value.CookieName, token, _cookieOptions);
    }

    public void DeleteCookie(IResponseCookies cookies)
    {
        cookies.Delete(jwtOptions.Value.CookieName, _cookieOptions);
    }
}