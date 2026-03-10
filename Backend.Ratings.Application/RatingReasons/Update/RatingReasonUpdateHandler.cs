using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Ratings.Application.RatingReasons.Update;

public class RatingReasonUpdateHandler(
    IRatingReasonRepository ratingReasonRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RatingReasonUpdateCommand, Unit>
{
    public async Task<Unit> Handle(RatingReasonUpdateCommand request, CancellationToken cancellationToken)
    {
        var reason = await ratingReasonRepository.GetAsync(request.Id, cancellationToken);
        if (reason is null)
        {
            throw new NotFoundAppException("Rating reason not found");
        }

        var changed = false;
        if (!string.IsNullOrWhiteSpace(request.Code))
        {
            reason.UpdateCode(request.Code);
            changed = true;
        }
        if (request.Value.HasValue)
        {
            reason.UpdateValue(request.Value.Value);
            changed = true;
        }
        if (request.IsActive.HasValue)
        {
            reason.UpdateIsActive(request.IsActive.Value);
            changed = true;
        }

        if (changed)
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return Unit.Value;
    }
}