
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;
public interface IRoomRepo : IGenericRepo<Room>
{
    Task<bool> CheckAvailableBetweenAsync(int roomId, DateTime start, DateTime end);
    Task<List<Room>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        );
}
