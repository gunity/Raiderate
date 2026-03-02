using FluentValidation;

namespace Backend.Players.Application.ApplyVote;

public class ApplyVoteValidator : AbstractValidator<ApplyVoteCommand>
{
    public ApplyVoteValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty();
        
        RuleFor(x => x.FromUserId)
            .NotEmpty();

        RuleFor(x => x.Delta)
            .NotEmpty();
        
        RuleFor(x => x.ReasonId)
            .GreaterThanOrEqualTo(0)
            .NotEmpty();
    }
}