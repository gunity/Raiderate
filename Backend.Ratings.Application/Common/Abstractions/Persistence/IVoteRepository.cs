using Backend.Ratings.Domain.Votes;

namespace Backend.Ratings.Application.Common.Abstractions.Persistence;

public interface IVoteRepository
{
    Task CreateAsync(Vote vote, CancellationToken ct = default);
    Task<bool> ExistsAsync(long playerId, long fromUserId, CancellationToken ct = default);
    Task<IReadOnlyList<Vote>> GetAllByPlayerIdReadonlyAsync(long playerId, int limit, CancellationToken ct = default);
}