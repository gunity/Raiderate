using Backend.Contracts.Grpc;

namespace Backend.Ratings.Application.Common.Abstractions;

public interface IPlayerClient
{
    Task<GetOrCreatePlayerReply> EnsurePlayerAsync(string nickname, CancellationToken ct);
}