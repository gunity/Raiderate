using Backend.Identity.Domain.Users;
using Backend.Shared.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Identity.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Login)
            .HasMaxLength(UserConstants.LoginMaxLength)
            .IsRequired();

        builder
            .HasIndex(x => x.Login)
            .IsUnique();

        builder
            .Property(x => x.PasswordHash)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.Role)
            .HasConversion(
                roleEnum => roleEnum.ToString().ToLowerInvariant(),
                roleString => Enum.Parse<AppRole>(roleString, ignoreCase: true))
            .HasMaxLength(UserConstants.MaxRoleLength)
            .HasDefaultValue(nameof(AppRole.User).ToLowerInvariant())
            .IsRequired();
    }
}