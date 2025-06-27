using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class RoomRepo : GenericRepo<Room>, IRoomRepo
{
    public RoomRepo(AppDbContext con) : base(con)
    {
    }
}
