
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Persistance.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new FeedbackConfiguration().Configure(builder.Entity<FeedBack>());
        new HotelConfiguration().Configure(builder.Entity<Hotel>());
        new ReservationConfiguration().Configure(builder.Entity<Reservation>());
        new RoomConfiguration().Configure(builder.Entity<Room>());
        new RoomImageConfiguration().Configure(builder.Entity<RoomImage>());
        new UserConfiguration().Configure(builder.Entity<User>());
        new PaymentConfiguration().Configure(builder.Entity<Payment>());
        new RoleConfiguration().Configure(builder.Entity<IdentityRole>());
      
    }

    public DbSet<Hotel> hotels { get; set; }

    public DbSet<Room> rooms { get; set; }

    public DbSet<FeedBack> feedBacks { get; set; }

    public DbSet<RoomImage> roomsImage { get; set; }

    public DbSet<Reservation> reservations { get; set; }

    public DbSet<RefreshToken> refreshTokens { get; set; }

    public DbSet<Payment> payments { get; set; }
    
}
