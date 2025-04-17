namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using System.Collections.Concurrent;
using Sports.Api.Model.Interface.Espn;

public abstract class EspnEventCollection : EspnCacheItem, IEspnEventCollection
{
    [JsonProperty("items")]
    public ConcurrentQueue<EspnApiUrl> EventUrls { get; set; }

    [JsonProperty("count")]
    public int EventCount { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}
