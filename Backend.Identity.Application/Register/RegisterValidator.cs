using Backend.Identity.Domain.Users;
using FluentValidation;

namespace Backend.Identity.Application.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
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