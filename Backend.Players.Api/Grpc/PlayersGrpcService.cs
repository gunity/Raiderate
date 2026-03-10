using Backend.Contracts.Grpc;
using Backend.Players.Application.GetOrCreate;
using Grpc.Core;
using MediatR;

namespace Backend.Players.Api.Grpc;

public class PlayersGrpcService(IMediator mediator) : PlayersService.PlayersServiceBase
{
    public override async Task<GetOrCreatePlayerReply> GetOrCreatePlayer(GetOrCreatePlayerRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new GetOrCreatePlayerCommand(request.Nickname), context.CancellationToken);
        return new GetOrCreatePlayerReply
        {
            Id = result.Id.ToString(),
            Nickname = result.Nickname
        };
    }
}