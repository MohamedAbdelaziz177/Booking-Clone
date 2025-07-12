using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class FeedbackRepo : GenericRepo<FeedBack>, IFeedbackRepo
{
    public FeedbackRepo(AppDbContext con) : base(con)
    {
    }

    public async Task<List<FeedBack>> GetFeedBacksByReservationIdAsync(int reservationId)
    {
        return await con.feedBacks.Where(f => f.ReservationId == reservationId)
            .Include(f => f.Reservation)
            .Include(f => f.user)
            .ToListAsync();
    }

    public async Task<List<FeedBack>> GetFeedBacksByRoomIdAsync(int roomId)
    {
        return await con.feedBacks
            .Include(f => f.Reservation)
            .ThenInclude(r => r.Room)
            .Where(f => f.Reservation.RoomId == roomId)
            .ToListAsync();
    }
}
