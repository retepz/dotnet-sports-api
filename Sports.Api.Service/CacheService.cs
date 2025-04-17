namespace Sports.Api.Service;

using Sports.Api.Model;
using Sports.Api.Model.Interface;
using Sports.Api.Service.Interface;
using FastHashes;
using System.Text;

public class CacheService(
    IMemoryCacheService memoryCacheService) : ICacheService
{
    public readonly int CurrentVersion = 3;

    public string GetUrlCacheKey<T>(T item)
        where T : ICacheItemUrl
    {
        var urlAsBytes = Encoding.UTF8.GetBytes(item.Url);
        var hasher = new FarmHash32();
        var urlHashBytes = hasher.ComputeHash(urlAsBytes);
        var urlHash = Convert.ToHexString(urlHashBytes);

        return urlHash;
    }

    public async Task<T?> Get<T>(string key, CacheCategory category)
        where T : class, ICacheItem
    {
        await Task.CompletedTask;

        var memoryCache = memoryCacheService.Get<T>(key);

        if (memoryCache != null && IsValid(memoryCache))
        {
            return memoryCache;
        }

        return null;
    }

    public async Task<T> Save<T>(T item, string key, CacheCategory category)
        where T : class, ICacheItem
    {
        await Task.CompletedTask;
        item.CurrentCacheVersion = CurrentVersion;
        item.CacheDate = DateTime.UtcNow;

        var memoryCache = memoryCacheService.Save(key, item);

        return memoryCache;
    }

    public void Remove<T>(string key, CacheCategory category)
        where T : class, ICacheItem
    {
        memoryCacheService.Remove(key);
    }

    private bool IsValid(ICacheItem cacheItem)
    {
        return CacheVersionIsValid(cacheItem) && CacheDateIsValid(cacheItem);
    }

    private bool CacheDateIsValid(ICacheItem cacheItem)
    {
        if (cacheItem.CacheNeverExpires)
        {
            return true;
        }

        var currentDate = DateTime.UtcNow;
        var datesMatch = cacheItem.CacheDate.HasValue
            && currentDate.Year == cacheItem.CacheDate.Value.Year
                && currentDate.Month == cacheItem.CacheDate.Value.Month
                && currentDate.Day == cacheItem.CacheDate.Value.Day;

        return datesMatch;
    }

    private bool CacheVersionIsValid(ICacheItem cacheItem)
    {
        return !cacheItem.CurrentCacheVersion.HasValue || cacheItem.CurrentCacheVersion.Value == CurrentVersion;
    }
}
