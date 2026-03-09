using Backend.Contracts.Grpc;
using Backend.Ratings.Application.Common.Abstractions;

namespace Backend.Ratings.Infrastructure.Grpc;

public class IdentityGrpcClient(IdentityService.IdentityServiceClient client) : IIdentityClient
{
    public async Task<GetLoginsByUserIdReply> GetLoginAsync(long[] ids, CancellationToken ct)
    {
        var request = new GetLoginsByUserIdRequest();
        request.Ids.AddRange(ids);
        
        return await client.GetLoginsByUserIdAsync(request, cancellationToken: ct);
    }
}