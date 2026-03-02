using System.Text.Json;
using Backend.Players.Api.Grpc;

namespace Backend.Players.Api;

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
        
        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapControllers();
        app.MapGrpcService<PlayersGrpcService>();

        return app;
    }
}