using Backend.Identity.Application.Common.Abstractions;
using Backend.Identity.Application.Common.Abstractions.Persistence;
using Backend.Identity.Infrastructure.Data;
using Backend.Identity.Infrastructure.Options;
using Backend.Identity.Infrastructure.Persistence;
using Backend.Identity.Infrastructure.Seeding;
using Backend.Identity.Infrastructure.Services;
using Backend.Shared.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Backend.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddServices();
        AddOptions();
        AddDatabase();

        return services;
        
        void AddServices()
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
        }
        
        void AddOptions()
        {
            configuration.AddEnvironmentVariables();
        
            services
                .AddOptions<DbOptions>()
                .Bind(configuration.GetSection("Db"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services
                .AddOptions<JwtOptions>()
                .Bind(configuration.GetSection("Jwt"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services
                .AddOptions<AdminOptions>()
                .Bind(configuration.GetSection("Admin"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
        
        void AddDatabase()
        {
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DbOptions>>().Value;
                options.UseNpgsql(dbOptions.Connection);
            });
            services.AddHostedService<AdminSeederHostedService>();
        }
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        try
        {
            var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            Environment.Exit(1);
        }
        
        return app;
    }
}