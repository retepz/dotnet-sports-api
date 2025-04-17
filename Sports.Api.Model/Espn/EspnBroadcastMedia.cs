namespace Sports.Api.Model.Espn;

public class EspnBroadcastMedia : EspnCacheItem
{
    public string ShortName { get; set; }

    public override bool CacheNeverExpires => true;
}
