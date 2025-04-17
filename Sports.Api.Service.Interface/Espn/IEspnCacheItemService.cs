namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Interface.Espn;
using Sports.Api.Model.Espn;

public interface IEspnCacheItemService<TCacheItem>
    where TCacheItem : class, IEspnCacheItem
{
    Task<TCacheItem?> Get(EspnApiUrl leagueApiUrl);
    Task<(bool FromCache, TCacheItem? Item)> GetCacheOrApiResult(EspnApiUrl leagueApiUrl);
}
