
using BookingClone.Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace BookingClone.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase cache;

    public RedisService(IDatabase cache)
    {
        this.cache = cache;
    }

    public async Task<T?> GetDataAsync<T>(string key) where T : class
    {
        var data = await cache.StringGetAsync(key);

        if (data.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<T>(data!);
        
    }

    public async Task SetDataAsync<T>(string key, T data) where T : class
    {
        await cache.StringSetAsync(key, JsonSerializer.Serialize(data));
    }

    public async Task RemoveDataAsync(string key)
    {
        await cache.KeyDeleteAsync(key);
    }

   
}
