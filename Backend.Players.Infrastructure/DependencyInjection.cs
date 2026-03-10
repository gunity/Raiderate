using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Players.Infrastructure.Consumers;
using Backend.Players.Infrastructure.Data;
using Backend.Players.Infrastructure.Options;
using Backend.Players.Infrastructure.Persistence;
using Backend.Shared.Options;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Backend.Players.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddServices();
        AddOptions();
        AddDatabase();
        AddMassTransit();

        return services;
        
        void AddServices()
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
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
                .AddOptions<RabbitMqOptions>()
                .Bind(configuration.GetSection("Rabbit"))
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
        }

        void AddMassTransit()
        {
            services.AddMassTransit(configurator =>
            {
                configurator.SetKebabCaseEndpointNameFormatter();
                
                configurator.AddConsumer<VoteCreatedConsumer>();
                
                configurator.AddEntityFrameworkOutbox<AppDbContext>(options =>
                {
                    options.UsePostgres();
                    options.UseBusOutbox();
                });
                
                configurator.AddConfigureEndpointsCallback((context, name, endpointConfigurator) =>
                {
                    endpointConfigurator.UseEntityFrameworkOutbox<AppDbContext>(context);
                });
                
                configurator.UsingRabbitMq((context, bus) =>
                {
                    var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                    bus.Host(rabbitMqOptions.Host, "/", hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMqOptions.User);
                        hostConfigurator.Password(rabbitMqOptions.Password);
                    });
                    bus.ConfigureEndpoints(context);
                });
            });
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