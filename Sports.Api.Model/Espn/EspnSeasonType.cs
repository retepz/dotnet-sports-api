namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnSeasonType : EspnCacheItem
{
    [JsonProperty("week")]
    public EspnWeek? CurrentWeek { get; set; }

    [JsonProperty("weeks")]
    public EspnWeek? CurrentWeeks { get; set; }

    [JsonProperty("type")]
    public EspnSeasonTypeId TypeId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("startDate")]
    public DateTime? StartDate { get; set; }

    [JsonProperty("endDate")]
    public DateTime? EndDate { get; set; }
}
