using FluentValidation;

namespace Backend.Identity.Application.GetLogin;

public class GetLoginValidator : AbstractValidator<GetLoginQuery>
{
    public GetLoginValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}