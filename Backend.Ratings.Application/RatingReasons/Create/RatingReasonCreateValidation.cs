using Backend.Ratings.Domain.RatingReasons;
using FluentValidation;

namespace Backend.Ratings.Application.RatingReasons.Create;

public class RatingReasonCreateValidation : AbstractValidator<RatingReasonCreateCommand>
{
    public RatingReasonCreateValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(RatingReasonConstants.MinCodeLength)
            .MaximumLength(RatingReasonConstants.MaxCodeLength);

        RuleFor(x => x.Value)
            .NotEmpty();
    }
}