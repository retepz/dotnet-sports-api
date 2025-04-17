namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnWeekEvent : EspnEventCollection
{
    [JsonProperty("$meta")]
    public EspnWeekEventMeta Meta { get; set; }
}