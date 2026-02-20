using Backend.Identity.Application.Login;
using Backend.Identity.Domain.Users;
using FluentValidation;

namespace Backend.Identity.Application.Register;

internal class RegisterValidation : AbstractValidator<LoginCommand>
{
    public RegisterValidation()
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