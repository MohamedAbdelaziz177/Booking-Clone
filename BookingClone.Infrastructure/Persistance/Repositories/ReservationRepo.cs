using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class ReservationRepo : GenericRepo<Reservation>, IReservationRepo
{
    public ReservationRepo(AppDbContext con) : base(con)
    {
    }
}
