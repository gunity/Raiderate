using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Shared.Auth;
using Backend.Shared.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Backend.Gateway.Infrastructure.Transforms;

internal sealed class UserHeadersTransformProvider(
    IOptions<JwtOptions> jwtOptions,
    ILogger<UserHeadersTransformProvider> logger
) : ITransformProvider
{
    private readonly TokenValidationParameters _validation = new()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Value.Issuer,

        ValidateAudience = true,
        ValidAudience = jwtOptions.Value.Audience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),

        ValidateLifetime = true,
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.FromSeconds(30),
    };

    public void ValidateRoute(TransformRouteValidationContext context) { }
    public void ValidateCluster(TransformClusterValidationContext context) { }

    public void Apply(TransformBuilderContext context)
    {
        context.AddRequestTransform(transformContext =>
        {
            var headers = transformContext.ProxyRequest.Headers;

            headers.Remove(AuthHeaders.UserId);
            headers.Remove(AuthHeaders.UserLogin);
            headers.Remove(AuthHeaders.UserRole);

            var http = transformContext.HttpContext;
            if (!http.Request.Cookies.TryGetValue(jwtOptions.Value.CookieName, out var token))
            {
                return ValueTask.CompletedTask;
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                return ValueTask.CompletedTask;
            }

            ClaimsPrincipal principal;
            try
            {
                principal = new JwtSecurityTokenHandler().ValidateToken(token, _validation, out _);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Token is invalid");
                return ValueTask.CompletedTask;
            }

            var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var login = principal.FindFirstValue(ClaimTypes.Name);
            var role = principal.FindFirstValue(ClaimTypes.Role);

            if (!string.IsNullOrWhiteSpace(id))
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserId, id);
            }
            if (!string.IsNullOrWhiteSpace(login))
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserLogin, login);
            }
            if (!string.IsNullOrWhiteSpace(role))
            {
                headers.TryAddWithoutValidation(AuthHeaders.UserRole, role);
            }

            return ValueTask.CompletedTask;
        });
    }
}