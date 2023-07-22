using Microsoft.Extensions.Caching.Memory;

namespace UMaTLMS.Infrastructure;

public class CacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void StoreEntities<T>(string key, List<T> value) where T : Entity
    {
        _cache.Set(key, value, DateTime.Now.AddMinutes(30));
    }

    public bool HasKey(string key)
    {
        return _cache.TryGetValue(key, out _);
    }

    public TValue? Get<TValue>(string key)
    {
        _ = _cache.TryGetValue(key, out TValue? value);
        return value;
    }

    public void Remove<T>()
    {
        _cache.Remove(typeof(T).Name);
    }
}
