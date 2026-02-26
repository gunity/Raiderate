using Backend.Ratings.Api;
using Backend.Ratings.Application;
using Backend.Ratings.Infrastructure;
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