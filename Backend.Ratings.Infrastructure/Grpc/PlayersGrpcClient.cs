using Backend.Contracts.Grpc;
using Backend.Ratings.Application.Common.Abstractions;

namespace Backend.Ratings.Infrastructure.Grpc;

public class PlayersGrpcClient(PlayersService.PlayersServiceClient client) : IPlayerClient
{
    public async Task<GetOrCreatePlayerReply> EnsurePlayerAsync(string nickname, CancellationToken ct)
    {
        return await client.GetOrCreatePlayerAsync(
            new GetOrCreatePlayerRequest { Nickname = nickname },
            cancellationToken: ct);
    }
}