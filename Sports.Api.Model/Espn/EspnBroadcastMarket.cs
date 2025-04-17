namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnBroadcastMarket : IEspnId
{
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Name { get; set; }
}
