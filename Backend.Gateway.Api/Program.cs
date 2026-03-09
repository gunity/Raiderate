using Backend.Gateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration);

builder.Build()
    .UseInfrastructure()
    .Run();
