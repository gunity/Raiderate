using Backend.Players.Domain.Players;

namespace Backend.Players.Application.Common.Abstractions.Persistence;

public interface IPlayerRepository
{
    Task<Player?> GetReadonlyByNicknameAsync(string nickname, CancellationToken ct = default);
    Task AddAsync(Player player, CancellationToken ct = default);
}