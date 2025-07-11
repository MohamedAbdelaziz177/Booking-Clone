
namespace BookingClone.Application.Contracts;

public interface IRedisService
{
    Task SetDataAsync<T>(string key, T data, string? tag = null) where T : class;

    Task<T?> GetDataAsync<T>(string key) where T : class;

    
    Task RemoveDataAsync(string key);

    Task RemoveByTagAsync(string tag);
} 
