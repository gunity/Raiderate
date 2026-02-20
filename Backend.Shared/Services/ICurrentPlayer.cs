namespace Backend.Shared.Services;

public interface ICurrentPlayer
{
    bool IsAuthenticated { get; }
    long Id { get; }
    string Login { get; }
    string Role { get; }
}