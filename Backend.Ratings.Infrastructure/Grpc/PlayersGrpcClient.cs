using Backend.Contracts.Grpc;
using Backend.Ratings.Application.Common.Abstractions;

namespace Backend.Ratings.Infrastructure.Grpc;

public class PlayersGrpcClient(PlayersService.PlayersServiceClient client) : IPlayerClient
{
    public async Task<EnsurePlayerReply> EnsurePlayerAsync(string nickname, CancellationToken ct)
    {
        return await client.EnsurePlayerAsync(
            new EnsurePlayerRequest { Nickname = nickname },
            cancellationToken: ct);
    }
}