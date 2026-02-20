using Backend.Identity.Api.Services;

namespace Backend.Identity.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        
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

        return app;
    }
}