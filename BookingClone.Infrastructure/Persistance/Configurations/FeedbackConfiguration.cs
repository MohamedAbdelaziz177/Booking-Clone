using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingClone.Infrastructure.Persistance.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> builder)
    {
        builder.Property(f => f.Comment).IsRequired(false)
            .HasMaxLength(300);

        builder.Property(f => f.Rating).IsRequired(true)
            .HasPrecision(3, 1)
            .HasAnnotation("Range", new Dictionary<String, Object>()
            { ["Minimum"] = 0, ["Maximum"] = 10 });
    }
}
