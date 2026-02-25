using Backend.Identity.Domain.Users;
using Backend.Identity.Infrastructure.Data;
using Backend.Identity.Infrastructure.Options;
using Backend.Shared.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Backend.Identity.Infrastructure.Seeding;

public class AdminSeederHostedService(
    IOptions<AdminOptions> adminOptions,
    IServiceScopeFactory  scopeFactory
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

        if (await context.Users.AnyAsync(x => x.Login == adminOptions.Value.Login, cancellationToken))
        {
            return;
        }

        var user = new User(adminOptions.Value.Login, adminOptions.Value.Password, AppRole.Admin);
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}