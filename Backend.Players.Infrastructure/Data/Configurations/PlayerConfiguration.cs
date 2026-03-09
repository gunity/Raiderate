using Backend.Players.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Players.Infrastructure.Data.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasIndex(x => x.Nickname)
            .IsUnique();
        
        builder
            .Property(x => x.Nickname)
            .HasMaxLength(PlayerConstants.MaxNameLength)
            .IsRequired();

        builder
            .Property(x => x.Rating)
            .IsRequired();

        builder
            .Property(x => x.VotesCount)
            .HasDefaultValue(0)
            .IsRequired();
    }
}