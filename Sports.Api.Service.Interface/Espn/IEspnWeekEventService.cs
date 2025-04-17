namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Espn;

public interface IEspnWeekEventService : IEspnCacheItemService<EspnWeekEvent>
{
    Task<(EspnWeek?, EspnWeekEvent)> Get(EspnLeague league, EspnSeason season);
    Task<EspnWeekEvent> Get(EspnLeague league, EspnWeek week);
    EspnApiUrl BuildEventsUrlFromLeagueUrl(EspnLeague leagueUrl);
}
