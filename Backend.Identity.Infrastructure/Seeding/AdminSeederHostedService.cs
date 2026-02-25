using Backend.Identity.Application.Common.Abstractions;
using Backend.Identity.Domain.Users;
using Backend.Identity.Infrastructure.Data;
using Backend.Identity.Infrastructure.Options;
using Backend.Shared.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Backend.Identity.Infrastructure.Seeding;

public class AdminSeederHostedService(
    IOptions<AdminOptions> adminOptions,
    IServiceScopeFactory  scopeFactory,
    IPasswordHasher passwordHasher,
    ILogger<AdminSeederHostedService> logger
) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(adminOptions.Value.Login))
        {
            return;
        }
        if (string.IsNullOrEmpty(adminOptions.Value.Password))
        {
            return;
        }

        var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var login = adminOptions.Value.Login.ToLowerInvariant().Trim();
        var exists = await context.Users.AnyAsync(x => x.Login == login, cancellationToken);
        if (exists)
        {
            logger.LogInformation("Admin with login=({login}) is already exists", login);
            return;
        }

        var passwordHash = passwordHasher.Hash(adminOptions.Value.Password);
        var user = new User(login, passwordHash, AppRole.Admin);
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Admin with login=({login}) has been created", login);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}