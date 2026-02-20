using Backend.Identity.Domain.Users;
using FluentValidation;

namespace Backend.Identity.Application.Login;

internal class LoginValidation : AbstractValidator<LoginCommand>
{
    public LoginValidation()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .MinimumLength(UserConstants.LoginMinLength)
            .MaximumLength(UserConstants.LoginMaxLength);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(UserConstants.PasswordMinLength)
            .MaximumLength(UserConstants.PasswordMaxLength);
    }
}