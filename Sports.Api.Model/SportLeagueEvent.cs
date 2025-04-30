namespace Sports.Api.Model;

using Sports.Api.Model.Espn;

public class SportLeagueEvent
{
    public SportLeagueEvent(
        EspnEvent espnEvent)
    {
        var espnCompetition = espnEvent?.Competition;

        var firstApiCompetitor = espnCompetition?.FirstCompetitor!;
        var secondApiCompetitor = espnCompetition?.SecondCompetitior!;

        Id = espnCompetition?.Id;
        FirstTeam = new(espnCompetition, firstApiCompetitor, espnEvent!.LeagueType);
        SecondTeam = new(espnCompetition, secondApiCompetitor, espnEvent.LeagueType);
        IsLive = espnCompetition?.CurrentStatus?.IsInProgress ?? false;
        IsFinished = espnCompetition?.CurrentStatus?.IsCompleted ?? false;
        IsInFuture = espnCompetition?.CurrentStatus?.IsInFuture ?? false;
        GameTime = espnCompetition?.Date;
        StatusType = espnCompetition?.CurrentStatus?.StatusType?.ShortDetail;
        DownDistance = espnCompetition?.CurrentSituation?.DownDistanceText;

        BroadcastStations = espnCompetition?
            .CurrentBroadcast?
            .Items
            .Select(item => item?.Media?.ShortName ?? item?.Station)
            .ToArray()!;
    }

    public string? Id { get; }
    public SportCompetitor FirstTeam { get; }
    public SportCompetitor SecondTeam { get; }
    public DateTime? GameTime { get; }
    public bool IsFinished { get; }
    public bool IsInFuture { get; }
    public bool IsLive { get; }
    public string? StatusType { get; }
    public string? DownDistance { get; }
    public string[]? BroadcastStations { get; }
}
