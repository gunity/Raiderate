using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Identity.Domain.Users;
using Backend.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Identity.Infrastructure.Persistence;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetReadonlyByLoginAsync(string login, CancellationToken ct = default)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login, ct);
    }

    public async Task<User?> GetReadonlyByIdAsync(long id, CancellationToken ct = default)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<bool> ExistsByLoginAsync(string login, CancellationToken ct = default)
    {
        return await context.Users
            .AsNoTracking()
            .AnyAsync(x => x.Login == login, ct);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }
}