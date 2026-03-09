using Backend.Contracts.Grpc;

namespace Backend.Ratings.Application.Common.Abstractions;

public interface IIdentityClient
{
    Task<GetLoginReply> GetLoginAsync(long id, CancellationToken ct = default);
}