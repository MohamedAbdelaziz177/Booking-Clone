using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingClone.Infrastructure.Persistance.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Rooms)
            .WithOne(x => x.Hotel)
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(30);


        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(200)
            .HasDefaultValue("Sample Hotel Description .. Sample Hotel Description .. Sample Hotel Description");

      
    }
}
