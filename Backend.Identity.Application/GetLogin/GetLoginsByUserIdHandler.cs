using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Shared.Exceptions;
using MediatR;

namespace Backend.Identity.Application.GetLogin;

public class GetLoginsByUserIdHandler(
    IUserRepository userRepository
) : IRequestHandler<GetLoginsByUserIdQuery, GetLoginsByUserIdResult>
{
    public async Task<GetLoginsByUserIdResult> Handle(GetLoginsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetReadonlyByIdsAsync(request.Ids, cancellationToken);
        
        var items = users
            .Select(x => new GetLoginsByUserIdItem(x.Id, x.Login))
            .ToArray();
        
        var foundIds = users
            .Select(x => x.Id)
            .ToHashSet();
        
        var missingIds = request.Ids
            .Where(x => !foundIds.Contains(x))
            .ToList();

        if (missingIds.Count > 0)
        {
            throw new NotFoundAppException($"Not found users with ID=({string.Join(", ", missingIds)})");
        }

        return new GetLoginsByUserIdResult(items);
    }
}