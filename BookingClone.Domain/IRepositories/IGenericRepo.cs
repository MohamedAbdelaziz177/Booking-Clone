

namespace BookingClone.Domain.IRepositories;
public interface IGenericRepo<T> where T : class
{
    Task AddAsync(T item);
    Task DeleteAsync(T item);
    Task DeleteAsync(int id);
    Task UpdateAsync(T item);
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        );

}
