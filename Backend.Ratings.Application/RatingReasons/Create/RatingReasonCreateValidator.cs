using Backend.Ratings.Domain.RatingReasons;
using FluentValidation;

namespace Backend.Ratings.Application.RatingReasons.Create;

public sealed class RatingReasonCreateValidator : AbstractValidator<RatingReasonCreateCommand>
{
    public RatingReasonCreateValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(RatingReasonConstants.MinCodeLength)
            .MaximumLength(RatingReasonConstants.MaxCodeLength);

        RuleFor(x => x.Value)
            .NotEmpty();
    }
}