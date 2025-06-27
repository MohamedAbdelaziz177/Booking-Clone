using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookingClone.Infrastructure.Persistance.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(r => r.BookingDate)
            .IsRequired(true)
            .HasDefaultValue(DateTime.Now);

        builder.Property(r => r.CheckInDate)
            .IsRequired(true);
        
        builder.Property(r => r.CheckOutDate)
            .IsRequired(true);

        builder.HasMany(x => x.FeedBacks)
            .WithOne(y => y.Reservation)
            .HasForeignKey(y => y.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        

    }
}
