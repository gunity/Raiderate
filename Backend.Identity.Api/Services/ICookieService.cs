namespace Backend.Identity.Api.Services;

public interface ICookieService
{
    void WriteCookie(IResponseCookies cookies, string accessToken, string refreshToken);
    void DeleteCookie(IResponseCookies cookies);
    bool TryGetRefreshToken(IRequestCookieCollection cookies, out string token);
}