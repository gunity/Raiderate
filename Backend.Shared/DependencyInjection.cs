using Backend.Shared.Auth;
using Backend.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddGatewayAuth(this IServiceCollection services)
    {
        AddServices();
        AddAuth();

        return services;

        void AddServices()
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentPlayer, CurrentPlayer>();
        }

        void AddAuth()
        {
            services
                .AddAuthentication(AuthenticationOptions.Scheme)
                .AddScheme<AuthenticationOptions, AuthenticationHandler>
                    (AuthenticationOptions.Scheme, ConfigureOptions);
            services
                .AddAuthorization();
        }

        void ConfigureOptions(AuthenticationOptions authenticationOptions)
        {
            // nothing here
        }
    }

    public static WebApplication UseGatewayAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}