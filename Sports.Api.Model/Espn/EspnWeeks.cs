namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnWeeks : EspnCacheItem
{
    [JsonProperty("items")]
    public EspnWeek[] AllWeeks { get; set; }
}
