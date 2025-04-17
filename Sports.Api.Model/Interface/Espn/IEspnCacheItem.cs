namespace Sports.Api.Model.Interface.Espn;

public interface IEspnCacheItem : IEspnApiUrl, ICacheItemUrl, IEspnItem
{
    bool IgnoreCache { get; }
}
