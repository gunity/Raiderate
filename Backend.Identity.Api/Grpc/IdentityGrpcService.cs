using Backend.Contracts.Grpc;
using Backend.Identity.Application.GetLogin;
using Grpc.Core;
using MediatR;

namespace Backend.Identity.Api.Grpc;

public class IdentityGrpcService(IMediator mediator) : IdentityService.IdentityServiceBase
{
    public override async Task<GetLoginReply> GetLogin(GetLoginRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new GetLoginQuery(request.Id), context.CancellationToken);
        return new GetLoginReply
        {
            Login = result.Login,
        };
    }
}