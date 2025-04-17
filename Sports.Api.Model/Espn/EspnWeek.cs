namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnWeek : EspnCacheItem
{
    public int Number { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [JsonProperty("events")]
    public EspnApiUrl EventsUrl { get; set; }

    [JsonProperty("text")]
    public string DisplayName { get; set; }
}
