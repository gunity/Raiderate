using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Domain.Votes;
using Backend.Ratings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Ratings.Infrastructure.Persistence;

public class VoteRepository(AppDbContext context) : IVoteRepository
{
    public async Task CreateAsync(Vote vote, CancellationToken ct = default)
    {
        await context.Votes.AddAsync(vote, ct);
    }

    public async Task<bool> ExistsAsync(long playerId, long fromUserId, CancellationToken ct = default)
    {
        return await context.Votes.AnyAsync(x => x.PlayerId == playerId && x.FromUserId == fromUserId, ct);
    }

    public async Task<IReadOnlyList<Vote>> GetAllByPlayerIdReadonlyAsync(
        long playerId, int limit,
        CancellationToken ct = default)
    {
        return await context.Votes
            .AsNoTracking()
            .Where(x => x.PlayerId == playerId)
            .Take(limit)
            .Include(x => x.Reason)
            .ToListAsync(ct);
    }
}