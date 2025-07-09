
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;
public interface IHotelRepo : IGenericRepo<Hotel>
{
      Task<List<Hotel>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        );
}
