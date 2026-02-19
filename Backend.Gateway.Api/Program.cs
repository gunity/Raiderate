using Backend.Gateway.Api;
using Backend.Gateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddInfrastructure(builder.Configuration);

builder.Build()
    .UseApi()
    .UseInfrastructure()
    .Run();
