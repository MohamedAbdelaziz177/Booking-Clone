using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class HotelRepo : GenericRepo<Hotel>, IHotelRepo
{
    public HotelRepo(AppDbContext con) : base(con)
    {
    }
}
