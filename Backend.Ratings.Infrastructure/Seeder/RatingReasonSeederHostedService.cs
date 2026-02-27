using Backend.Ratings.Domain.RatingReasons;
using Backend.Ratings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.Ratings.Infrastructure.Seeder;

public class RatingReasonSeederHostedService(
    IServiceScopeFactory scopeFactory
) : IHostedService
{
    public async Task StartAsync(CancellationToken ct)
    {
        var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (await context.RatingReasons.AnyAsync(ct))
        {
            return;
        }

        await context.RatingReasons.AddRangeAsync(
            new RatingReason("revived", 3),
            new RatingReason("cover_fire", 2),
            new RatingReason("shared_loot", 2),
            new RatingReason("fair_play", 1),
            new RatingReason("insults", -1),
            new RatingReason("attempted_kill", -3),
            new RatingReason("betrayal_kill", -5)
        );

        await context.SaveChangesAsync(ct);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}