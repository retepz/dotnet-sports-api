namespace Sports.Api.Service.Interface;

using Sports.Api.Model;

public interface IGetSportLeagueService
{
    Task<SportLeagueSeason?> GetSeason(LeagueType leagueType);
    Task<SportLeague[]?> GetLeagues(SportType sportType);
    Task<SportLeagueWeek?> GetCurrentWeek(LeagueType leagueType);
    Task<SportLeagueEvent[]?> GetWeekEvents(LeagueType leagueType);
}
