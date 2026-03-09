using Backend.Contracts.Grpc;
using Backend.Identity.Application.GetLogin;
using Grpc.Core;
using MediatR;

namespace Backend.Identity.Api.Grpc;

public class IdentityGrpcService(IMediator mediator) : IdentityService.IdentityServiceBase
{
    public override async Task<GetLoginsByUserIdReply> GetLoginsByUserId(GetLoginsByUserIdRequest request, ServerCallContext context)
    {
        var ids = request.Ids.ToArray();
        var result = await mediator.Send(new GetLoginsByUserIdQuery(ids), context.CancellationToken);

        var reply = new GetLoginsByUserIdReply();
        reply.Items.AddRange(result.Items.Select(x => new UserLoginItem
        {
            Id = x.Id,
            Login = x.Login
        }));
        return reply;
    }
}