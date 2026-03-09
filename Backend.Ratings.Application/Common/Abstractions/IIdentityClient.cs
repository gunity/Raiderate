using Backend.Contracts.Grpc;

namespace Backend.Ratings.Application.Common.Abstractions;

public interface IIdentityClient
{
    Task<GetLoginsByUserIdReply> GetLoginAsync(long[] ids, CancellationToken ct = default);
}