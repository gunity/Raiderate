namespace Backend.Ratings.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        
        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}