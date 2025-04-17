namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnCompetitor : EspnCacheItem, IEspnId
{
    [JsonProperty("team")]
    public EspnApiUrl TeamUrl { get; set; }

    public string Id { get; set; }

    public bool Winner { get; set; }

    public string HomeAway { get; set; }

    [JsonProperty("score")]
    public EspnApiUrl ScoreUrl { get; set; }

    [JsonIgnore]
    public EspnTeam CurrentTeam { get; set; }

    [JsonIgnore]
    public EspnScore CurrentScore { get; set; }

    [JsonIgnore]
    public bool IsHome => HomeAway != null && HomeAway.Contains("home", StringComparison.InvariantCultureIgnoreCase);

    public override bool CacheNeverExpires => true;
}
