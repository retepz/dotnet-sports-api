namespace Sports.Api.Service.Interface;

using Sports.Api.Model;
using Sports.Api.Model.Interface;

public interface ICacheService
{
    Task<T?> Get<T>(string key, CacheCategory category)
        where T : class, ICacheItem;
    Task<T> Save<T>(T item, string key, CacheCategory category)
        where T : class, ICacheItem;
    void Remove<T>(string key, CacheCategory category)
        where T : class, ICacheItem;

    string GetUrlCacheKey<T>(T item)
        where T : ICacheItemUrl;
}
