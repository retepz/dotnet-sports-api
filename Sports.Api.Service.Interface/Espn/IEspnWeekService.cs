namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Espn;

public interface IEspnWeekService : IEspnCacheItemService<EspnWeek>
{
    Task<EspnWeek?> GetCurrentWeek(EspnLeague league, EspnSeason season);
    Task<EspnWeeks?> GetWeeks(EspnApiUrl espnApiUrl);
}
