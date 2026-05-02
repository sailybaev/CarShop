namespace CarShopFinal.Domain.Interfaces;

public interface ICacheService
{
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T?>> valueFactory, TimeSpan ttl);

}