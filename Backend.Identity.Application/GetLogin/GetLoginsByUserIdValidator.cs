using FluentValidation;

namespace Backend.Identity.Application.GetLogin;

public class GetLoginsByUserIdValidator : AbstractValidator<GetLoginsByUserIdQuery>
{
    public GetLoginsByUserIdValidator()
    {
        RuleFor(x => x.Ids)
            .NotEmpty();
    }
}