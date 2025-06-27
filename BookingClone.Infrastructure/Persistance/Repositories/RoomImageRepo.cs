using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class RoomImageRepo : GenericRepo<RoomImage>, IRoomImageRepo
{
    public RoomImageRepo(AppDbContext con) : base(con)
    {
    }
}
