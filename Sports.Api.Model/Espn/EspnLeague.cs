namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnLeague : EspnCacheItem, IEspnId
{
    public string? Id { get; set; }

    public LeagueType LeagueType { get; set; }

    public string? ShortName { get; set; }

    public string? DisplayName { get; set; }

    public string? Name { get; set; }

    public string? Abbreviation { get; set; }

    public EspnApiLogo[]? Logos { get; set; }

    [JsonProperty("season")]
    public EspnLeagueSeason? CurrentSeason { get; set; }

    [JsonIgnore]
    public EspnApiLogo? Logo => Logos?.FirstOrDefault(logo => logo.IsDark) ?? Logos?.FirstOrDefault();
}
