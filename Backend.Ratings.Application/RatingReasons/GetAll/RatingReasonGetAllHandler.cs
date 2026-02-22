using Backend.Ratings.Application.Common.Abstractions.Persistence;
using MediatR;

namespace Backend.Ratings.Application.RatingReasons.GetAll;

public class RatingReasonGetAllHandler(
    IRatingReasonRepository ratingReasonRepository
) : IRequestHandler<RatingReasonGetAllQuery, RatingReasonGetAllResult>
{
    public async Task<RatingReasonGetAllResult> Handle(RatingReasonGetAllQuery request, CancellationToken cancellationToken)
    {
        var ratingReasons = await ratingReasonRepository.GetAllAsync(cancellationToken);

        return new RatingReasonGetAllResult(ratingReasons);
    }
}