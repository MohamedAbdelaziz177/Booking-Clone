
using BookingClone.Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BookingClone.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IDistributedCache cache;

    public RedisService(IDistributedCache cache)
    {
        this.cache = cache;
    }

    public T? GetData<T>(string key)
    {
        var data = cache.Get(key);

        if (data == null) 
            return default(T);

        return JsonSerializer.Deserialize<T>(data);
        
    }

    public void SetData<T>(string key, T data)
    {

        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        cache.SetString(key, JsonSerializer.Serialize(data), options);
    }
}
