using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Identity.Application.GetLogin;

public class GetLoginHandler(
    IUserRepository userRepository
) : IRequestHandler<GetLoginQuery, GetLoginResult>
{
    public async Task<GetLoginResult> Handle(GetLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetReadonlyByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundAppException($"User with ID={request.Id} not found");
        }

        return new GetLoginResult(user.Login);
    }
}