using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Identity.Infrastructure.Data;

namespace Backend.Identity.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}