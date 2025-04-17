namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public abstract class EspnCacheItem : EspnApiUrl, IEspnCacheItem
{
    public EspnCacheItem() { }
    public EspnCacheItem(string url)
    {
        Url = url;
    }

    public int? CurrentCacheVersion { get; set; }
    public DateTime? CacheDate { get; set; }

    [JsonIgnore]
    public virtual bool CacheNeverExpires { get; }

    [JsonIgnore]
    public virtual bool IgnoreCache => false;
}
