using Backend.Contracts.Grpc;
using Backend.Players.Application.EnsurePlayer;
using Grpc.Core;
using MediatR;

namespace Backend.Players.Api.Grpc;

public class PlayersGrpcService(IMediator mediator) : PlayersService.PlayersServiceBase
{
    public override async Task<EnsurePlayerReply> EnsurePlayer(EnsurePlayerRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new EnsurePlayerCommand(request.Nickname), context.CancellationToken);
        return new EnsurePlayerReply
        {
            Id = result.Id,
            Nickname = result.Nickname
        };
    }
}