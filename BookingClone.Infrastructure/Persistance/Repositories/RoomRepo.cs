using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

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

    public async Task<List<Room>> GetAvaliableRoomsBetween(DateTime start,
        DateTime end,
        int? hotelId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int pageIdx = 1,
        int pageSize = 3,
        string sortField = "Id",
        string sortDir = "asc")
    {
        Expression<Func<Room, bool>> expr1, expr2, expr3, expr4;

        expr1 = r =>
            !con.reservations.Any(x => x.RoomId == r.Id
            && x.CheckInDate < end
            && x.CheckOutDate > start);

        expr2 = (hotelId != null) ? r => r.HotelId == hotelId : r => true;

        expr3 = (minPrice != null) ? r => r.PricePerNight >= minPrice : r => true;

        expr4 = (maxPrice != null) ? r => r.PricePerNight <= maxPrice : r => true;


        IQueryable<Room> rooms = con.rooms.Where(expr1)
            .Where(expr2)
            .Where(expr3)
            .Where(expr4);
        
        if(sortField.ToUpper() == "PRICE")
        {
            if (sortDir.ToUpper() == "ASC")
                rooms.OrderBy(r => r.PricePerNight);
            
            rooms.OrderByDescending(r => r.PricePerNight);
        }

        else
        rooms.OrderByDescending (r => r.PricePerNight);

        rooms.Skip(pageSize * (pageIdx - 1)).Take(pageSize);

        return await rooms.ToListAsync();
        
    }

    public override async Task<List<Room>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        )
    {
        var query = con.rooms.Skip((pageIdx - 1) * pageSize).Take(pageSize);

        if (sortField.ToUpper() == "PRICE")
        {
            if (sortDir.ToUpper() == "DESC")
                query = query.OrderByDescending(x => x.PricePerNight);

            else query = query.OrderBy(x => x.PricePerNight);
        }

        else query = query.OrderByDescending(x => x.Id);

        return await query.Include(r => r.RoomImages).Include(r => r.Hotel).ToListAsync();
    }


    public override async Task<Room?> GetByIdAsync(int id)
    {
        return await con
            .rooms
            .Include(r => r.RoomImages)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

}
