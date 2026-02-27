using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Domain.RatingReasons;
using Backend.Ratings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Ratings.Infrastructure.Persistence;

public class RatingReasonRepository(AppDbContext context) : IRatingReasonRepository
{
    public async Task<RatingReason?> GetAsync(long id, CancellationToken ct = default)
    {
        return await context.RatingReasons
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IEnumerable<RatingReason>> GetAllAsync(CancellationToken ct = default)
    {
        return await context.RatingReasons
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<IEnumerable<RatingReason>> GetAllActiveAsync(CancellationToken ct = default)
    {
        return await context.RatingReasons
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task AddAsync(RatingReason reason, CancellationToken ct = default)
    {
        await context.RatingReasons
            .AddAsync(reason, ct);
    }

    public Task<bool> ExistsByCode(string code, CancellationToken ct = default)
    {
        return context.RatingReasons
            .AnyAsync(x => x.Code == code, ct);
    }

    public Task<bool> ExistsAndActiveById(long id, CancellationToken ct = default)
    {
        return context.RatingReasons
            .AnyAsync(x => x.Id == id && x.IsActive, ct);
    }
}