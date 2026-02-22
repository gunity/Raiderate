using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Infrastructure.Data;

namespace Backend.Ratings.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}