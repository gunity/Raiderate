using Backend.Identity.Api;
using Backend.Identity.Application;
using Backend.Identity.Infrastructure;
using Backend.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGatewayAuth()
    .AddApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Build()
    .UseGatewayAuth()
    .UseApi()
    .UseInfrastructure()
    .Run();