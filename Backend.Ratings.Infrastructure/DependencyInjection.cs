using Backend.Contracts.Grpc;
using Backend.Ratings.Application.Common.Abstractions;
using Backend.Ratings.Application.Common.Abstractions.Persistence;
using Backend.Ratings.Infrastructure.Data;
using Backend.Ratings.Infrastructure.Grpc;
using Backend.Ratings.Infrastructure.Options;
using Backend.Ratings.Infrastructure.Persistence;
using Backend.Ratings.Infrastructure.Seeder;
using Backend.Shared.Options;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Backend.Ratings.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddServices();
        AddOptions();
        AddDatabase();
        AddGrpc();
        AddMassTransit();

        return services;
        
        void AddServices()
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRatingReasonRepository, RatingReasonRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
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
                .AddOptions<DestinationOptions>()
                .Bind(configuration.GetSection("Destination"))
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
            services.AddHostedService<RatingReasonSeederHostedService>();
        }
        
        void AddGrpc()
        {
            services.AddGrpcClient<PlayersService.PlayersServiceClient>((serviceProvider, options) =>
            {
                var destinationOptions = serviceProvider.GetRequiredService<IOptions<DestinationOptions>>().Value;
                options.Address = new Uri(destinationOptions.PlayersGrpcUrl);
            });
            services.AddScoped<IPlayerClient, PlayersGrpcClient>();
            
            services.AddGrpcClient<IdentityService.IdentityServiceClient>((serviceProvider, options) =>
            {
                var destinationOptions = serviceProvider.GetRequiredService<IOptions<DestinationOptions>>().Value;
                options.Address = new Uri(destinationOptions.IdentityGrpcUrl);
            });
            services.AddScoped<IIdentityClient, IdentityGrpcClient>();
        }

        void AddMassTransit()
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingRabbitMq((context, bus) =>
                {
                    var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                    bus.Host(rabbitMqOptions.Host, "/", hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMqOptions.User);
                        hostConfigurator.Password(rabbitMqOptions.Password);
                    });
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