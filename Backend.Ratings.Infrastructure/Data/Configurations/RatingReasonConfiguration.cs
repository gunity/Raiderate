using Backend.Ratings.Domain.RatingReasons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Ratings.Infrastructure.Data.Configurations;

public class RatingReasonConfiguration : IEntityTypeConfiguration<RatingReason>
{
    public void Configure(EntityTypeBuilder<RatingReason> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Code)
            .HasMaxLength(RatingReasonConstants.MaxCodeLength)
            .IsRequired();

        builder
            .HasIndex(x => x.Code)
            .IsUnique();

        builder
            .Property(x => x.Value)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasDefaultValue(true)
            .IsRequired();
    }
}