using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingClone.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Reservations)
            .WithOne(r => r.user)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.Property(u => u.PasswordResetOtp)
        //    .IsRequired(false);
        //
        //builder.Property(u => u.PasswordResetOtp)
        //   .IsRequired(false);

    }
}
