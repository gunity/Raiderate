using Backend.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Backend.Identity.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}