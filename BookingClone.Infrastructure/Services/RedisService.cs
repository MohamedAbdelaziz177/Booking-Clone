
using BookingClone.Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace BookingClone.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase cache;

    public RedisService(IConnectionMultiplexer multiplexer)
    {
        this.cache = multiplexer.GetDatabase();
    }

    public async Task<T?> GetDataAsync<T>(string key) where T : class
    {
        var data = await cache.StringGetAsync(key);

        if (data.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<T>(data!);
        
    }

    public async Task SetDataAsync<T>(string key, T data, string? tag = null) where T : class
    {
        await cache.StringSetAsync(key, JsonSerializer.Serialize(data));

        if(tag != null)
            await cache.SetAddAsync(tag, key);
    }

    public async Task RemoveByTagAsync(string tag)
    {
        var keys = await cache.SetMembersAsync(tag);
        var keysAsStrings = keys.Select(x => x.ToString()).ToList();

        foreach (var key in keysAsStrings)
            await cache.KeyDeleteAsync(key);

        await cache.KeyDeleteAsync(tag);
    }

    public async Task RemoveDataAsync(string key)
    {
        await cache.KeyDeleteAsync(key);
    }

   
}
