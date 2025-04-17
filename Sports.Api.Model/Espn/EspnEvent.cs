namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnEvent : EspnCacheItem, IEspnId
{
    [JsonProperty("competitions")]
    public EspnApiUrl[] CompetitionUrls { get; set; }

    public string Id { get; set; }

    [JsonIgnore]
    public IEnumerable<EspnCompetition> CurrentCompetitions { get; set; }

    public LeagueType LeagueType { get; set; }

    [JsonIgnore]
    public EspnCompetition Competition => CurrentCompetitions.First();

    [JsonProperty("week")]
    public EspnApiUrl WeekUrl { get; set; }
}
