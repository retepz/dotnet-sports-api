namespace Sports.Api.Service.Espn;

using Sports.Api.Service.Interface;
using Sports.Api.Model;
using Sports.Api.Service.Interface.Espn;
using Sports.Api.Model.Interface.Espn;
using Sports.Api.Model.Espn;

public class EspnCacheItemService<TCacheItem>(
    ICacheService cacheService,
    IEspnApiService<TCacheItem> espnApiService) : IEspnCacheItemService<TCacheItem>
    where TCacheItem : class, IEspnCacheItem
{
    protected readonly ICacheService _cacheService = cacheService;

    public async virtual Task<TCacheItem?> Get(EspnApiUrl leagueApiUrl)
    {
        var (_, result) = await GetCacheOrApiResult(leagueApiUrl);

        return result;
    }

    public async virtual Task<(bool FromCache, TCacheItem? Item)> GetCacheOrApiResult(
        EspnApiUrl leagueApiUrl)
    {
        var cacheKey = GetCacheKey(leagueApiUrl);

        var fromCache = await GetFromCache(cacheKey);
        if (fromCache != null)
        {
            return (true, fromCache);
        }

        var fromApi = await TryGetFromApi(leagueApiUrl);
        if (fromApi == null)
        {
            return (false, null);
        }

        var updatedApiCache = await SetCache(fromApi, cacheKey);
        return (false, updatedApiCache);
    }

    protected async Task<TCacheItem?> GetFromCache(string cacheKey)
    {
        var fromCache = await _cacheService.Get<TCacheItem>(cacheKey, CacheCategory.Json);
        if (fromCache != null && !fromCache.IgnoreCache)
        {
            return fromCache;
        }

        return null;
    }

    protected string GetCacheKey(EspnApiUrl leagueApiUrl)
    {
        var tempForCache = new EspnTempCacheItem(leagueApiUrl.Url);
        var cacheKey = _cacheService.GetUrlCacheKey(tempForCache);
        return cacheKey;
    }

    protected async Task<TCacheItem?> SetCache(TCacheItem fromApi, string cacheKey)
    {
        var updatedItem = await _cacheService.Save(fromApi, cacheKey, CacheCategory.Json);
        return updatedItem;
    }

    protected virtual async Task<TCacheItem?> GetFromApi(EspnApiUrl espnApiUrl)
    {
        return await espnApiService.Get(espnApiUrl);
    }

    protected async Task<TCacheItem?> TryGetFromApi(EspnApiUrl espnApiUrl)
    {
        try
        {
            return await GetFromApi(espnApiUrl);
        }
        catch
        {
            return null;
        }
    }
}
