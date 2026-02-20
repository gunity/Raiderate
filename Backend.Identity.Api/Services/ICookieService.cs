namespace Backend.Identity.Api.Services;

public interface ICookieService
{
    void WriteCookie(IResponseCookies cookies, string token);
    void DeleteCookie(IResponseCookies cookies);
}