using Backend.Identity.Application.Common.Abstractions;
using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Identity.Application.Login;

internal sealed class LoginHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
    
) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var login = request.Login.Trim().ToLowerInvariant();
        
        var user = await userRepository.GetReadonlyByLoginAsync(login, cancellationToken);
        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAppException("Invalid login or password");
        }

        return new LoginResult(user.Id, user.Login, user.Role.ToString());
    }
}