
using Backend.Ratings.Domain.RatingReasons;

namespace Backend.Ratings.Application.Common.Abstractions.Persistence;

public interface IRatingReasonRepository
{
    Task<RatingReason?> GetAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<RatingReason>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<RatingReason>> GetAllActiveAsync(CancellationToken ct = default);

    Task AddAsync(RatingReason reason, CancellationToken ct = default);
}