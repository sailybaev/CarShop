using System.Text.Json;
using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Persistance.Redis;

namespace CarShopFinal.Infrastructure.Services;

    

public class CacheService:ICacheService
{
    private readonly IRedis _redis;

    public CacheService(IRedis redis)
    {
        _redis = redis;
    }
    
    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T?>> valueFactory, TimeSpan ttl)
    {
        var cache = await _redis.GetAsync(key);
        if (cache != null)
            return JsonSerializer.Deserialize<T>(cache);
        var res = await valueFactory();
        if (res != null)
        {
            var json= JsonSerializer.Serialize(res);
            await _redis.SetAsync(key, json, ttl);
        }
        return res;
    }
}
