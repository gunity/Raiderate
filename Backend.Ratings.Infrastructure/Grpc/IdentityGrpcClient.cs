using Backend.Contracts.Grpc;
using Backend.Ratings.Application.Common.Abstractions;

namespace Backend.Ratings.Infrastructure.Grpc;

public class IdentityGrpcClient(IdentityService.IdentityServiceClient client) : IIdentityClient
{
    public async Task<GetLoginReply> GetLoginAsync(long id, CancellationToken ct)
    {
        return await client.GetLoginAsync(
            new GetLoginRequest { Id = id },
            cancellationToken: ct);
    }
}