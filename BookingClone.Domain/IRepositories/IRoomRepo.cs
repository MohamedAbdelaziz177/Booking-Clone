
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;
public interface IRoomRepo : IGenericRepo<Room>
{
    Task<bool> CheckAvailableBetweenAsync(int roomId, DateTime start, DateTime end);
}
