using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class RoomRepo : GenericRepo<Room>, IRoomRepo
{
    public RoomRepo(AppDbContext con) : base(con)
    {
    }

    public async Task<bool> CheckAvailableBetweenAsync(int roomId, DateTime start, DateTime end)
    {
        
        return await con.reservations.AnyAsync(x => x.RoomId == roomId 
        && ( (x.CheckOutDate > start && x.CheckOutDate < end ) || (x.CheckInDate < end && x.CheckInDate > start )));

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
