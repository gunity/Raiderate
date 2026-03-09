using Backend.Players.Api;
using Backend.Players.Application;
using Backend.Players.Infrastructure;
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