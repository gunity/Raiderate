namespace Backend.Shared.Services;

public interface ICurrentPlayer
{
    bool IsAuthenticated { get; }
    Guid Id { get; }
    string Login { get; }
    string Role { get; }
}