using Backend.Shared.Exceptions;
using Backend.Shared.Services;
using MediatR;

namespace Backend.Identity.Application.Self;

public class SelfHandler(
    ICurrentPlayer currentPlayer
) : IRequestHandler<SelfQuery, SelfResult>
{
    public Task<SelfResult> Handle(SelfQuery request, CancellationToken cancellationToken)
    {
        if (!currentPlayer.IsAuthenticated)
        {
            throw new UnauthorizedException();
        }

        var result = new SelfResult(currentPlayer.Id, currentPlayer.Login, currentPlayer.Role);
        return Task.FromResult(result);
    }
}