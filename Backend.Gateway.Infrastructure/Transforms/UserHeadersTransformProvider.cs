using System.Security.Claims;
using Backend.Shared.Auth;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Backend.Gateway.Infrastructure.Transforms;

internal sealed class UserHeadersTransformProvider : ITransformProvider
{
    public void ValidateRoute(TransformRouteValidationContext context) { }

    public void ValidateCluster(TransformClusterValidationContext context) { }

    public void Apply(TransformBuilderContext context)
    {
        context.AddRequestTransform(static transformContext =>
        {
            var headers = transformContext.ProxyRequest.Headers;

            headers.Remove(AuthHeaders.UserId);
            headers.Remove(AuthHeaders.UserLogin);
            headers.Remove(AuthHeaders.UserRoles);

            var user = transformContext.HttpContext.User;
            if (!(user.Identity?.IsAuthenticated ?? false))
            {
                return ValueTask.CompletedTask;
            }

            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(id))
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserId, id);
            }

            var login = user.FindFirstValue(ClaimTypes.Name);
            if (!string.IsNullOrWhiteSpace(login))
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserLogin, login);
            }
            
            var roles = user
                .FindAll(ClaimTypes.Role)
                .Select(x => x.Value)
                .Distinct()
                .ToArray();
            if (roles.Length > 0)
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserRoles, string.Join(',', roles));
            }
            
            return ValueTask.CompletedTask;
        });
    }
}