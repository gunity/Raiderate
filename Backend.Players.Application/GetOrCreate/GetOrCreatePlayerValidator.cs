using Backend.Players.Domain.Players;
using FluentValidation;

namespace Backend.Players.Application.GetOrCreate;

public class GetOrCreatePlayerValidator : AbstractValidator<GetOrCreatePlayerCommand>
{
    public GetOrCreatePlayerValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MinimumLength(PlayerConstants.MinNameLength)
            .MaximumLength(PlayerConstants.MaxNameLength);
    }
}