namespace Sports.Api.Model.Espn;

public class EspnTempCacheItem(string url) : EspnCacheItem(url)
{
    public EspnTempCacheItem(EspnApiUrl espnApiUrl)
        : this(espnApiUrl.Url)
    {
    }
}
