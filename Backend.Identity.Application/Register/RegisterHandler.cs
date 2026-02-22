using Backend.Identity.Application.Common.Abstractions;
using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Identity.Domain.Users;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Identity.Application.Register;

public class RegisterHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork
) : IRequestHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var login = request.Login.Trim().ToLowerInvariant();
        if (await userRepository.ExistsByLoginAsync(login, cancellationToken))
        {
            throw new AlreadyExistsAppException("Login already exists");
        }
        
        var passwordHash = passwordHasher.Hash(request.Password);
        var user = new User(login, passwordHash);
        
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterResult(user.Id, user.Login, user.Role.ToString());
    }
}