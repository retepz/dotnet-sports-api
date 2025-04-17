namespace Sports.Api.Service.Espn;

using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface;
using Sports.Api.Service.Interface.Espn;

public class EspnWeekEventService(
    ICacheService cacheService,
    IEspnApiService<EspnWeekEvent> espnApiService,
    IEspnWeekService weekService)
    : EspnCacheItemService<EspnWeekEvent>(cacheService, espnApiService), IEspnWeekEventService
{

    public async Task<(EspnWeek?, EspnWeekEvent)> Get(EspnLeague league, EspnSeason season)
    {
        var weekEventUrl = BuildEventsUrlFromLeagueUrl(league);
        var espnWeekEvent = await GetUpdatedEvents(weekEventUrl, league);
        if (season.CurrentWeeksUrl == null)
        {
            return (null, espnWeekEvent);
        }

        if (espnWeekEvent.Meta?.Parameters?.Week == null || espnWeekEvent.Meta?.Parameters?.Week.Length != 1)
        {
            return (null, espnWeekEvent);
        }

        var seasonWeekUrls = await weekService.GetWeeks(season.CurrentWeeksUrl);
        if (seasonWeekUrls == null || seasonWeekUrls.AllWeeks.Length == 0)
        {
            return (null, espnWeekEvent);
        }

        var weekNumberFromMeta = int.Parse(espnWeekEvent.Meta.Parameters.Week[0]);
        foreach (var seasonWeekUrl in seasonWeekUrls.AllWeeks)
        {
            var week = await weekService.Get(seasonWeekUrl);
            if (week == null || week.Number != weekNumberFromMeta)
            {
                continue;
            }

            return (week, espnWeekEvent);
        }

        return (null, espnWeekEvent);
    }

    public async Task<EspnWeekEvent> Get(EspnLeague league, EspnWeek week)
    {
        var useLeagueUrl = new LeagueType[]
        {
            LeagueType.CollegeBasketball,
            LeagueType.NFL,
            LeagueType.NBAGLeague,
            LeagueType.NBA
        };

        if (useLeagueUrl.Contains(league.LeagueType))
        {
            var tempWeek = new EspnWeek()
            {
                EventsUrl = BuildEventsUrlFromLeagueUrl(league),
            };

            return await GetUpdatedEvents(tempWeek.EventsUrl, league);
        }

        if (week.EventsUrl == null)
        {
            week.EventsUrl = BuildEventsUrl(week, league.LeagueType);
            return await GetUpdatedEvents(week.EventsUrl, league);
        }

        var updatedEvents = await GetUpdatedEvents(week.EventsUrl, league);
        if (updatedEvents?.EventUrls == null || !updatedEvents.EventUrls.Any())
        {
            week.EventsUrl = BuildEventsUrlFromLeagueUrl(league);
            return await GetUpdatedEvents(week.EventsUrl, league);
        }

        return updatedEvents;
    }

    // Old potentially overcomplicated setup
    private async Task<EspnWeekEvent> GetUpdatedEvents(
        EspnApiUrl weekEventUrl,
        EspnLeague leagueUrl)
    {
        var cacheKey = GetCacheKey(weekEventUrl);
        var weekEventsFromCache = await GetFromCache(cacheKey);
        if (weekEventsFromCache != null)
        {
            var fromApi = await GetFromApi(weekEventUrl);
            if (fromApi != null && fromApi.EventCount != weekEventsFromCache.EventCount)
            {
                _cacheService.Remove<EspnWeekEvent>(cacheKey, CacheCategory.Json);
                return await GetUpdatedEvents(weekEventUrl, leagueUrl);
            }

            return weekEventsFromCache;
        }

        var weekEventsFromApi = await GetFromApi(weekEventUrl);
        if (weekEventsFromApi != null)
        {
            if (weekEventsFromApi.PageCount > 1)
            {
                await SetWeekEventUrlsFromApi(weekEventUrl, weekEventsFromApi);
            }

            return await SetCache(weekEventsFromApi, cacheKey);
        }

        return null;
    }

    private EspnApiUrl BuildEventsUrl(EspnWeek week, LeagueType leagueType)
    {
        var weekUri = new Uri(week.Url);
        var queryAddition = leagueType == LeagueType.CollegeFootball ? "&groups=80" : string.Empty;

        return BuildEventsUrl(weekUri, queryAddition);
    }

    private EspnApiUrl BuildEventsUrl(Uri uri, string queryAddition = "")
    {
        return new EspnApiUrl
        {
            Url = $"https://{uri.Host}{uri.AbsolutePath}/{EspnApiService<EspnWeekEvent>.EventsPath}{uri.Query}{queryAddition}"
        };
    }

    public EspnApiUrl BuildEventsUrlFromLeagueUrl(EspnLeague leagueUrl)
    {
        var weekUri = new Uri(leagueUrl.Url);
        return BuildEventsUrl(weekUri);
    }

    private async Task SetWeekEventUrlsFromApi(
        EspnApiUrl weekEventsUrl,
        EspnWeekEvent espnWeekEvent)
    {
        var startingPage = espnWeekEvent.PageIndex + 1;
        for (var currentPage = startingPage; currentPage <= espnWeekEvent.PageCount; currentPage++)
        {
            try
            {
                var nextUrl = new EspnApiUrl
                {
                    Url = $"{weekEventsUrl.Url}&page={currentPage}"
                };
                var nextEvents = await GetFromApi(nextUrl);
                Parallel.ForEach(nextEvents.EventUrls, espnWeekEvent.EventUrls.Enqueue);
            }
            catch
            {
            }
        }
    }
}
