using Backend.Players.Domain.Players;
using FluentValidation;

namespace Backend.Players.Application.EnsurePlayer;

public class EnsurePlayerValidator : AbstractValidator<EnsurePlayerCommand>
{
    public EnsurePlayerValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MinimumLength(PlayerConstants.MinNameLength)
            .MaximumLength(PlayerConstants.MaxNameLength);
    }
}