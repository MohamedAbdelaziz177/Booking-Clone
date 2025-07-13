using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class RoomRepo : GenericRepo<Room>, IRoomRepo
{
    public RoomRepo(AppDbContext con) : base(con)
    {
    }


    public async Task<List<Room>> GetByHotelId(int hotelId)
    {
        return await con.rooms.Where(r => r.HotelId == hotelId).ToListAsync();
    }
    public async Task<bool> CheckAvailableBetweenAsync(int roomId, DateTime start, DateTime end)
    {
        
        return !await con.reservations.AnyAsync(x => x.RoomId == roomId 
        && x.CheckOutDate > start
        && x.CheckInDate < end );
    }

    public async Task<List<Room>> GetAvaliableRoomsBetween(DateTime start, DateTime end, int? hotelId = null)
    {
        Expression<Func<Room, bool>> expr1, expr2;

        expr1 = r =>
            !con.reservations.Any(x => x.RoomId == r.Id
            && x.CheckInDate < end
            && x.CheckOutDate > start);

        expr2 = (hotelId != null) ? r => r.HotelId == hotelId : r => true;

        return await con.rooms.Where(expr1).Where(expr2).ToListAsync();
        
    }

    public override async Task<List<Room>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        )
    {
        var query = con.rooms.Include(r => r.RoomImages).Skip((pageIdx - 1) * pageSize).Take(pageSize);

        if (sortField.ToUpper() == "PRICE")
        {
            if (sortDir.ToUpper() == "DESC")
                query = query.OrderByDescending(x => x.PricePerNight);

            else query = query.OrderBy(x => x.PricePerNight);
        }

        else query = query.OrderByDescending(x => x.Id);

        return await query.ToListAsync();
    }


    public override async Task<Room?> GetByIdAsync(int id)
    {
        return await con
            .rooms
            .Include(r => r.RoomImages)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

}
