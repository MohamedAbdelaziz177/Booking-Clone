using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingClone.Infrastructure.Persistance.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        

        builder.HasMany(r => r.FeedBacks)
            .WithOne(f => f.Room)
            .HasForeignKey(f => f.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.RoomImages)
            .WithOne(i => i.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Reservations)
            .WithOne(rs => rs.Room)
            .HasForeignKey(x => x.RoomId) 
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PricePerNight)
            .HasPrecision(8, 2);
    }
}
