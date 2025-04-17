namespace Sports.Api.Service.Espn;

using Sports.Api.Service.Interface;
using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface.Espn;

public class EspnSportService(
    ICacheService cacheService,
    IEspnApiService<EspnSport> espnApiService,
    IEspnLeagueService espnLeagueService,
    IEspnCacheItemService<EspnSportLeagues> espnSportLeaguesService)
    : EspnCacheItemService<EspnSport>(cacheService, espnApiService), IEspnSportService
{
    private const string _baseUrl = "https://sports.core.api.espn.com/v2/sports";

    public async Task<EspnSport?> Get(SportType sportType)
    {
        var espnUrl = new EspnApiUrl
        {
            Url = $"{_baseUrl}/{sportType.ToString().ToLower()}"
        };

        return await Get(espnUrl);
    }

    public async Task<IList<EspnLeague>?> GetLeagues(EspnSport espnSport)
    {
        if (espnSport.GetLeaguesUrl == null)
        {
            return null;
        }

        EspnSportLeagues? leagueUrls = null;
        try
        {
            leagueUrls = await espnSportLeaguesService.Get(espnSport.GetLeaguesUrl);
        }
        catch
        {
        }

        if (leagueUrls == null)
        {
            return null;
        }

        var result = new List<EspnLeague>();
        foreach (var espnLeagueUrl in leagueUrls.Items)
        {
            var espnLeague = await espnLeagueService.Get(espnLeagueUrl);
            if (espnLeague == null)
            {
                continue;
            }

            result.Add(espnLeague);
        }

        return result;
    }
}
