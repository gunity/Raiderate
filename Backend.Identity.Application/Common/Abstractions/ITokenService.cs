namespace Backend.Identity.Application.Common.Abstractions;

public interface ITokenService
{
    string IssueToken(long id, string login, string role);
}