using Backend.Ratings.Domain.Votes;
using FluentValidation;

namespace Backend.Ratings.Application.Votes.Create;

public class VotesCreateValidator : AbstractValidator<VotesCreateCommand>
{
    public VotesCreateValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MinimumLength(VoteConstants.MinNicknameLength)
            .MaximumLength(VoteConstants.MaxNicknameLength);

        RuleFor(x => x.ReasonId)
            .NotEmpty();

        RuleFor(x => x.Comment)
            .MaximumLength(VoteConstants.MaxCommentLength);
    }
}