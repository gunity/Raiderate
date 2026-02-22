using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Domain.RatingReasons;
using MediatR;

namespace Backend.Ratings.Application.RatingReasons.Create;

public class RatingReasonCreateHandler(
    IRatingReasonRepository ratingReasonRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RatingReasonCreateCommand, RatingReasonCreateResult>
{
    public async Task<RatingReasonCreateResult> Handle(RatingReasonCreateCommand request, CancellationToken cancellationToken)
    {
        var ratingReason = new RatingReason(request.Code, request.Value);

        await ratingReasonRepository.AddAsync(ratingReason, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RatingReasonCreateResult(ratingReason.Id);
    }
}