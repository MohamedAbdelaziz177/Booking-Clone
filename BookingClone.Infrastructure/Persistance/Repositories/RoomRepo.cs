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


    //public async Task<List<Room>> GetAvailableBetweenAsync(DateTime start, DateTime end)
    //{
    //    
    //}
}
