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
            headers.Remove(AuthHeaders.UserRole);

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
            
            var role = user.FindFirstValue(ClaimTypes.Role);
            if (!string.IsNullOrWhiteSpace(role))
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserRole, role);
            }
            
            return ValueTask.CompletedTask;
        });
    }
}