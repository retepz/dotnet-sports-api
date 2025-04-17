namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnLeagueSeason : EspnApiUrl, IEspnId
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public EspnSeasonType Type { get; set; }
    public int Year { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [JsonIgnore]
    public EspnApiUrl? CurrentWeekUrl => Type.CurrentWeek;

    [JsonIgnore]
    public EspnApiUrl? CurrentWeeksUrl => Type.CurrentWeeks;

    [JsonIgnore]
    public bool IsOffSeason => !DatesAreInSeason || TypeIsOffSeason;

    [JsonIgnore]
    public bool DatesAreInSeason => DateTime.UtcNow.Ticks > StartDate.Ticks && DateTime.UtcNow.Ticks < EndDate.Ticks;

    [JsonIgnore]
    public bool TypeIsOffSeason => Type != null && Type.TypeId == EspnSeasonTypeId.Off;
}
