namespace Sports.Api.Service;

using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface;
using Sports.Api.Service.Interface.Espn;

public sealed class GetSportLeagueService(
    IEspnSportService espnSportService,
    IEspnLeagueService espnLeagueService)
    : IGetSportLeagueService
{
    private readonly Dictionary<LeagueType, SportType> _leagueSportMap = new()
    {
        { LeagueType.NFL, SportType.Football },
        { LeagueType.NHL, SportType.Hockey },
        { LeagueType.MLB, SportType.Baseball },
        { LeagueType.UFL, SportType.Football },
        { LeagueType.XFL, SportType.Football },
        { LeagueType.CFL, SportType.Football },
        { LeagueType.CollegeBasketball, SportType.Basketball },
        { LeagueType.CollegeFootball, SportType.Football },
        { LeagueType.NBA, SportType.Basketball },
        { LeagueType.NBAGLeague, SportType.Basketball },
        { LeagueType.WNBA, SportType.Basketball }
    };

    public async Task<SportLeagueSeason?> GetSeason(LeagueType leagueType) 
    {
        var sportType = _leagueSportMap[leagueType];
        var espnLeagues = await GetEspnLeagues(sportType);
        if (espnLeagues == null)
        {
            return null;
        }

        var espnLeague = espnLeagues.SingleOrDefault(l => l.LeagueType == leagueType);
        if (espnLeague == null)
        {
            return null;
        }

        var espnSeason = await espnLeagueService.GetCurrentSeason(espnLeague);
        if (espnSeason == null)
        {
            return null;
        }

        return new(espnSeason);
    }

    public async Task<SportLeague[]?> GetLeagues(SportType sportType)
    {
        var espnLeagues = await GetEspnLeagues(sportType);
        if(espnLeagues == null)
        {
            return null;
        }

        return [.. espnLeagues.Select(el => new SportLeague(el))];
    }
    
    public async Task<SportLeagueWeek?> GetCurrentWeek(LeagueType leagueType)
    {
        var sportType = _leagueSportMap[leagueType];
        var espnLeagues = await GetEspnLeagues(sportType);
        if (espnLeagues == null)
        {
            return null;
        }

        var espnLeague = espnLeagues.SingleOrDefault(l => l.LeagueType == leagueType);
        if (espnLeague == null)
        {
            return null;
        }

        var espnSeason = await espnLeagueService.GetCurrentSeason(espnLeague);
        if (espnSeason == null || espnSeason.IsOffSeason)
        {
            return null;
        }

        var espnWeek = await espnLeagueService.GetCurrentWeek(espnLeague, espnSeason);
        if(espnWeek == null) 
        {
            return null;
        }

        return new(espnWeek);
    }

    public async Task<SportLeagueEvent[]?> GetWeekEvents(LeagueType leagueType)
    {
        var sportType = _leagueSportMap[leagueType];
        var espnLeagues = await GetEspnLeagues(sportType);
        if (espnLeagues == null)
        {
            return null;
        }

        var espnLeague = espnLeagues.SingleOrDefault(l => l.LeagueType == leagueType);
        if (espnLeague == null)
        {
            return null;
        }

        var espnSeason = await espnLeagueService.GetCurrentSeason(espnLeague);
        if (espnSeason == null || espnSeason.IsOffSeason)
        {
            return null;
        }

        var espnWeek = await espnLeagueService.GetCurrentWeek(espnLeague, espnSeason);
        if (espnWeek == null)
        {
            return null;
        }

        var (_, espnWeekEvents) = await espnLeagueService.GetWeekEvents(espnLeague, espnSeason, espnWeek);
        if(espnWeekEvents == null)
        {
            return null;
        }

        return [.. espnWeekEvents.Select(we => new SportLeagueEvent(we))];
    }

    private async Task<IList<EspnLeague>?> GetEspnLeagues(SportType sportType)
    {
        var espnSport = await espnSportService.Get(sportType);
        if (espnSport == null)
        {
            return null;
        }

        var espnLeagues = await espnSportService.GetLeagues(espnSport);
        if (espnLeagues == null || espnLeagues.Count == 0)
        {
            return null;
        }

        return espnLeagues;
    }
}
