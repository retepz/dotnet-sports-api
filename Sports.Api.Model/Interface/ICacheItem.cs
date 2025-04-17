namespace Sports.Api.Model.Interface;

public interface ICacheItem
{
    int? CurrentCacheVersion { get; set; }
    DateTime? CacheDate { get; set; }
    bool CacheNeverExpires { get; }
}
