using Backend.Ratings.Application.Common.Abstractions.Persistence;
using MediatR;

namespace Backend.Ratings.Application.RatingReasons.GetAllActive;

public class RatingReasonGetAllActiveHandler(
    IRatingReasonRepository ratingReasonRepository
) : IRequestHandler<RatingReasonGetAllActiveQuery, RatingReasonGetAllActiveResult>
{
    public async Task<RatingReasonGetAllActiveResult> Handle(RatingReasonGetAllActiveQuery request, CancellationToken cancellationToken)
    {
        var result = await ratingReasonRepository.GetAllActiveAsync(cancellationToken);
        
        return new RatingReasonGetAllActiveResult(result);
    }
}