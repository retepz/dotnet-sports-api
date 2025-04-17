namespace Sports.Api.Model;

using Sports.Api.Model.Espn;

public sealed record SportLeagueSeasonType
{
    public SportLeagueSeasonType(EspnSeasonType espnSeasonType)
    {
        SeasonType = espnSeasonType.TypeId;
        Name = espnSeasonType.Name;
        StartDate = espnSeasonType.StartDate;
        EndDate = espnSeasonType.EndDate;
    }

    public EspnSeasonTypeId SeasonType { get; }
    public string Name { get; }
    public DateTime? StartDate { get; }
    public DateTime? EndDate { get; }
}
