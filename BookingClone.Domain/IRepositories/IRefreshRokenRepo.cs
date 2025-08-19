
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;

public interface IRefreshRokenRepo : IGenericRepo<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<List<RefreshToken>> GetByUserIdAsync(string userId);
}
