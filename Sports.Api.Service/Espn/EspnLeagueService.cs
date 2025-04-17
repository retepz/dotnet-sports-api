namespace Sports.Api.Service.Espn;

using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface;
using Sports.Api.Service.Interface.Espn;

public class EspnLeagueService(
    IEspnApiService<EspnLeague> espnApiService,
    IEspnCacheItemService<EspnSeason> espnSeasonService,
    ICacheService cacheService,
    IEspnWeekService espnWeekService,
    IEspnWeekEventService weekEventService,
    IEspnEventService espnEventService)
    : EspnCacheItemService<EspnLeague>(cacheService, espnApiService), IEspnLeagueService
{
    private readonly Dictionary<string, LeagueType> _leagueAbbreviationNameMap = new()
    {
        { "ncaam", LeagueType.CollegeBasketball },
        { "ncaaf", LeagueType.CollegeFootball }
    };

    protected override async Task<EspnLeague?> GetFromApi(EspnApiUrl leagueApiUrl)
    {
        var league = await base.GetFromApi(leagueApiUrl);

        if (league == null)
        {
            return null;
        }

        league.LeagueType = TryGetLeagueType(league);

        return league;
    }

    public async Task<EspnSeason?> GetCurrentSeason(EspnLeague league)
    {
        if (league.CurrentSeason == null)
        {
            return null;
        }

        return await espnSeasonService.Get(league.CurrentSeason);
    }

    public async Task<EspnWeek?> GetCurrentWeek(EspnLeague league, EspnSeason season)
    {
        return await espnWeekService.GetCurrentWeek(league, season);
    }

    public async Task<(EspnWeek?, IEnumerable<EspnEvent>?)> GetWeekEvents(EspnLeague league, EspnSeason season, EspnWeek? week)
    {
        if (week == null)
        {
            var (newWeek, newWeekWeekEvent) = await weekEventService.Get(league, season);
            var newWeekEspnEvents = await espnEventService.Get(league.LeagueType, newWeekWeekEvent);
            return (newWeek, newWeekEspnEvents);
        }

        var weekEvent = await weekEventService.Get(league, week);
        var espnEvents = await espnEventService.Get(league.LeagueType, weekEvent);

        return (null, espnEvents);
    }

    private LeagueType TryGetLeagueType(EspnLeague league)
    {
        if (TryParseFromAbbrev(league, out var abbrevLeague))
        {
            return abbrevLeague;
        }

        if (TryParseLeagueType(league.ShortName, out var shortNameLeague))
        {
            return shortNameLeague;
        }

        if (TryParseLeagueType(league.Name, out var nameLeague))
        {
            return nameLeague;
        }

        if (TryParseLeagueType(league.DisplayName, out var displayNameLeague))
        {
            return displayNameLeague;
        }

        return LeagueType.None;
    }
    private bool TryParseFromAbbrev(EspnLeague league, out LeagueType result)
    {
        var toParse = league.Abbreviation?.ToLower() ?? string.Empty;

        if (_leagueAbbreviationNameMap.TryGetValue(toParse, out result))
        {
            return true;
        }

        return TryParseLeagueType(league.Abbreviation, out result);
    }

    private bool TryParseLeagueType(string? input, out LeagueType result)
    {
        var toParse = input ?? string.Empty;

        if (Enum.TryParse(toParse, ignoreCase: true, out result))
        {
            return true;
        }

        var stringReplacedValue = toParse.Replace(" ", string.Empty);

        if (Enum.TryParse(stringReplacedValue, ignoreCase: true, out result))
        {
            return true;
        }

        result = LeagueType.None;
        return false;
    }
}
