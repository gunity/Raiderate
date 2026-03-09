using System.Text.Json;
using Backend.Identity.Api.Grpc;
using Backend.Identity.Api.Services;

namespace Backend.Identity.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
            });
        services.AddGrpc();
        
        AddServices();

        return services;
        
        void AddServices()
        {
            services.AddScoped<ICookieService, CookieService>();
        }
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapControllers();
        app.MapGrpcService<IdentityGrpcService>();

        return app;
    }
}