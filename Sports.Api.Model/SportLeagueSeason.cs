namespace Sports.Api.Model;

using Sports.Api.Model.Espn;

public sealed record SportLeagueSeason
{
    public SportLeagueSeason(EspnSeason espnSeason)
    {
        Id = espnSeason.Id;
        DisplayName = espnSeason.DisplayName;
        Type = new(espnSeason.Type);
        Year = espnSeason.Year;
        StartDate = espnSeason.StartDate;
        EndDate = espnSeason.EndDate;
    }

    public string Id { get; }
    public string DisplayName { get; }
    public SportLeagueSeasonType Type { get; }
    public int Year { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public bool IsOffSeason => !DatesAreInSeason || TypeIsOffSeason;
    public bool DatesAreInSeason => DateTime.UtcNow.Ticks > StartDate.Ticks && DateTime.UtcNow.Ticks < EndDate.Ticks;
    public bool TypeIsOffSeason => Type != null && Type.SeasonType == EspnSeasonTypeId.Off;
}
