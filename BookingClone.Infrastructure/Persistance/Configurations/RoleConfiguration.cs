
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingClone.Infrastructure.Persistance.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(_roles);
    }

    private static readonly string StaticGuid1 = "1e4458b0-5e59-4b9d-a2c3-dbc97d3ff1aa";
    private static readonly string StaticGuid2 = "debc5c97-54de-4e1c-8c8e-2a408d13df49";

    private List<IdentityRole> _roles = new List<IdentityRole>
    {
        new IdentityRole(){Id = StaticGuid1, Name = "User", NormalizedName = "USER"} ,
        new IdentityRole(){Id = StaticGuid2, Name = "Admin", NormalizedName = "ADMIN"}
    };
}
