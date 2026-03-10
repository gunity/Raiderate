using Backend.Identity.Domain.Users;

namespace Backend.Identity.Application.Common.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetReadonlyByLoginAsync(string login, CancellationToken ct = default);
    Task<IReadOnlyList<User>> GetReadonlyByIdsAsync(Guid[] ids, CancellationToken ct = default);
    Task<bool> ExistsByLoginAsync(string login, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken cancellationToken);
}