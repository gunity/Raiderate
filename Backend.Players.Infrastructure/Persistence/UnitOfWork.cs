using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Players.Infrastructure.Data;

namespace Backend.Players.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}