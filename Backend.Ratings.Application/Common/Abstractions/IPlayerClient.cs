using Backend.Contracts.Grpc;

namespace Backend.Ratings.Application.Common.Abstractions;

public interface IPlayerClient
{
    Task<EnsurePlayerReply> EnsurePlayerAsync(string nickname, CancellationToken ct);
}