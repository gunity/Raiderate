using Backend.Gateway.Infrastructure.Options;
using Backend.Gateway.Infrastructure.Proxy;
using Backend.Gateway.Infrastructure.Proxy.Common;
using Backend.Gateway.Infrastructure.Transforms;
using Backend.Shared.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions();
        AddReverseProxy();

        return services;
        
        void AddOptions()
        {
            Options<JwtOptions>("Jwt");
            Options<DestinationOptions>("Destination");
            return;

            void Options<T>(string sectionName) where T : class
            {
                services
                    .AddOptions<T>()
                    .Bind(configuration.GetSection(sectionName))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
            }
        }
        
        void AddReverseProxy()
        {
            services.AddSingleton<IProxyConfigProvider, ProxyConfigProvider>();
        
            services.AddSingleton<IProxyModule, IdentityProxyModule>();

            services
                .AddReverseProxy()
                .AddTransforms<UserHeadersTransformProvider>();
        }
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.MapReverseProxy();
        
        return app;
    }
}