namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Espn;

public interface IEspnLeagueService : IEspnCacheItemService<EspnLeague>
{
    Task<EspnSeason?> GetCurrentSeason(EspnLeague league);
    Task<EspnWeek?> GetCurrentWeek(EspnLeague league, EspnSeason season);
    Task<(EspnWeek?, IEnumerable<EspnEvent>?)> GetWeekEvents(EspnLeague league, EspnSeason season, EspnWeek? week);
}
