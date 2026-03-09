using Backend.Ratings.Domain.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Ratings.Infrastructure.Data.Configurations;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.PlayerId)
            .IsRequired();

        builder
            .Property(x => x.FromUserId)
            .IsRequired();

        builder
            .Property(x => x.ReasonId)
            .IsRequired();

        builder
            .HasOne(x => x.Reason)
            .WithMany()
            .HasForeignKey(x => x.ReasonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.Comment)
            .HasMaxLength(VoteConstants.MaxCommentLength);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();
        
        builder
            .HasIndex(x => new { x.PlayerId, x.FromUserId })
            .IsUnique();
    }
}