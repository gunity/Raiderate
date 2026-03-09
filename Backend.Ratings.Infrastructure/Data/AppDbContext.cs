using Backend.Ratings.Domain.RatingReasons;
using Backend.Ratings.Domain.Votes;
using Microsoft.EntityFrameworkCore;

namespace Backend.Ratings.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RatingReason> RatingReasons => Set<RatingReason>();
    public DbSet<Vote> Votes => Set<Vote>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}