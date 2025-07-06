using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class ReservationRepo : GenericRepo<Reservation>, IReservationRepo
{
    public ReservationRepo(AppDbContext con) : base(con)
    {
    }

    public async Task<List<Reservation>> GetByUserIdAsync(string userId)
    {
        return await con.reservations.Where(con => con.UserId == userId).ToListAsync();
    }
}
