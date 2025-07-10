

using System.Linq.Expressions;

namespace BookingClone.Domain.IRepositories;
public interface IGenericRepo<T> where T : class
{
    Task AddAsync(T item);
    Task DeleteAsync(T item);
    Task DeleteAsync(int id);
    Task UpdateAsync(T item);
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();

    Task<List<T>> Filter(Expression<Func<T, bool>> predicate,
        string includes = "",
        int PageIdx = 1,
        int PageSize = 8
        );

    Task<List<T>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        );

}
