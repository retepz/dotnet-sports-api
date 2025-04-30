namespace Sports.Api.Model;

using Sports.Api.Model.Espn;
using System;

public class SportCompetitor
{
    public SportCompetitor(
        EspnCompetition? espnCompetition,
        EspnCompetitor? espnCompetitor,
        LeagueType leagueType)
    {
        Location = espnCompetitor?.CurrentTeam?.Location ?? string.Empty;
        Name = GetName(espnCompetitor, Location, leagueType);
        Logo = GetLogo(espnCompetitor);
        Score = GetScore(espnCompetition, espnCompetitor);
        IsHome = espnCompetitor?.IsHome ?? false;
        HasPossession = GetHasPossession(espnCompetition, espnCompetitor);
        Record = espnCompetitor?.CurrentTeam?.CurrentRecord?.DisplayValue;
        Color = espnCompetitor?.CurrentTeam.Color;
    }

    public string Name { get; }
    public string Location { get; }
    public string Logo { get; }
    public string Score { get; }
    public bool IsHome { get; }
    public bool HasPossession { get; }
    public string? Record { get; }
    public string? Color { get; }

    private static string GetName(EspnCompetitor? apiCompetitor,
        string location,
        LeagueType leagueType)
    {
        var apiName = apiCompetitor?.CurrentTeam?.Name;
        var apiShortName = apiCompetitor?.CurrentTeam?.ShortDisplayName;

        if (leagueType == LeagueType.CollegeFootball)
        {
            return apiName ?? apiShortName ?? string.Empty;
        }

        var shortNameIsLocation = !string.IsNullOrEmpty(location)
            && !string.IsNullOrEmpty(apiShortName)
            && location.Contains(apiShortName, StringComparison.InvariantCultureIgnoreCase);

        if (!string.IsNullOrEmpty(apiName) && shortNameIsLocation)
        {
            return apiName;
        }
        return apiShortName ?? apiName ?? string.Empty;
    }

    private static string GetLogo(EspnCompetitor? espnCompetitor)
    {
        return espnCompetitor?.CurrentTeam?.ScoreboardLogo?.Url ?? espnCompetitor?.CurrentTeam?.DefaultLogo?.Url ?? string.Empty;
    }

    private static string GetScore(
        EspnCompetition? espnCompetition,
        EspnCompetitor? espnCompetitor)
    {
        if (espnCompetition != null && espnCompetitor != null)
        {
            var apiCompInFuture = espnCompetition != null && espnCompetition.CurrentStatus.IsInFuture;
            if (apiCompInFuture)
            {
                return string.Empty;
            }
            return espnCompetitor?.CurrentScore?.DisplayValue ?? string.Empty;
        }

        return string.Empty;
    }

    private static bool GetHasPossession(EspnCompetition? espnCompetition, EspnCompetitor? espnCompetitor)
    {
        if (espnCompetition?.CompetitorWithPosession == null || espnCompetitor == null)
        {
            return false;
        }

        return espnCompetition.CompetitorWithPosession.Id == espnCompetitor.Id;
    }
}
