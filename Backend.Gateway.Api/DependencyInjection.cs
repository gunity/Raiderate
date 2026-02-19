using Backend.Gateway.Api.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Backend.Gateway.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        AddAuth();
        
        return services;

        void AddAuth()
        {
            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearer>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
        }
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseAuthentication();
        
        return app;
    }
}