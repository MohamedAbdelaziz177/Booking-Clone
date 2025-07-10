
namespace BookingClone.Application.Contracts;

public interface IRedisService
{
    void SetData<T>(string key, T data);

    T? GetData<T>(string key);
     
}
