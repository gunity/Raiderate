using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Players.Domain.Players;
using Backend.Players.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Players.Infrastructure.Persistence;

public class PlayerRepository(AppDbContext context) : IPlayerRepository
{
    public async Task<Player?> GetReadonlyByNicknameAsync(string nickname, CancellationToken ct = default)
    {
        return await context.Players
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Nickname == nickname, ct);
    }

    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await context.Players
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IReadOnlyList<Player>> GetReadonlyTopAsync(int limit, CancellationToken ct = default)
    {
        return await context.Players
            .AsNoTracking()
            .OrderByDescending(x => x.Rating)
            .Take(limit)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Player>> GetReadonlyBottomAsync(int limit, CancellationToken ct = default)
    {
        return await context.Players
            .AsNoTracking()
            .OrderBy(x => x.Rating)
            .Take(limit)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Player player, CancellationToken ct = default)
    {
        await context.Players.AddAsync(player, ct);
    }
}