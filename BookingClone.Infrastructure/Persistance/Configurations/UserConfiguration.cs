using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingClone.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(r => r.User)
            .HasForeignKey(u => u.UserId);

        builder.HasMany(u => u.FeedBacks)
            .WithOne(f => f.user)
            .HasForeignKey(u => u.UserId);

        //builder.Property(u => u.PasswordResetOtp)
        //    .IsRequired(false);
        //
        //builder.Property(u => u.PasswordResetOtp)
        //   .IsRequired(false);

    }
}
