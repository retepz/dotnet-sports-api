namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnSport : EspnCacheItem, IEspnId
{
    public string Id { get; set; }

    public string DisplayName { get; set; }

    [JsonProperty("leagues")]
    public EspnApiUrl GetLeaguesUrl { get; set; }

    public EspnApiLogo[] Logos { get; set; }

    [JsonIgnore]
    public EspnApiLogo? Logo => Logos?.FirstOrDefault(logo => logo.IsDark) ?? Logos?.FirstOrDefault();
}
