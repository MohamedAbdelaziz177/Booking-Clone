

using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;
public interface IReservationRepo : IGenericRepo<Reservation>
{
    Task<List<Reservation>> GetByUserIdAsync(string userId);
}
