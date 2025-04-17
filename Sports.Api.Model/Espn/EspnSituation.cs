namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnSituation : EspnCacheItem
{
    public string DownDistanceText { get; set; }
    public int HomeTimeouts { get; set; }
    public int AwayTimeouts { get; set; }

    [JsonProperty("team")]
    public EspnApiUrl CurrentTeamPossessionUrl { get; set; }
}
