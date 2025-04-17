namespace Sports.Api.Service.Espn;

using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface;
using Sports.Api.Service.Interface.Espn;

public class EspnWeekService(
    ICacheService cacheService,
    IEspnApiService<EspnWeek> espnApiService,
    IEspnCacheItemService<EspnWeeks> espnWeeksService)
    : EspnCacheItemService<EspnWeek>(cacheService, espnApiService), IEspnWeekService
{
    private readonly LeagueType[] _useCurrentWeeksLeagues = [LeagueType.CollegeFootball];
    public async Task<EspnWeek?> GetCurrentWeek(EspnLeague league, EspnSeason season)
    {
        if (season.CurrentWeekUrl != null)
        {
            return await Get(season.CurrentWeekUrl);
        }

        if (season.CurrentWeeksUrl == null || !_useCurrentWeeksLeagues.Contains(league.LeagueType))
        {
            return null;
        }

        var result = await GetWeeks(season.CurrentWeeksUrl);
        if (result == null || result.AllWeeks == null || result.AllWeeks.Length == 0)
        {
            return null;
        }

        var lastWeek = result.AllWeeks[result.AllWeeks.Length - 1];
        return lastWeek;
    }

    public async Task<EspnWeeks?> GetWeeks(EspnApiUrl espnApiUrl)
    {
        return await espnWeeksService.Get(espnApiUrl);
    }
}
