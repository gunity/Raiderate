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
}