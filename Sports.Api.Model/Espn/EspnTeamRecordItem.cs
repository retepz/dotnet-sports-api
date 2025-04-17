namespace Sports.Api.Model.Espn;

using Sports.Api.Model.Interface.Espn;

public class EspnTeamRecordItem : EspnCacheItem, IEspnId
{
    public string Id { get; set; }
    public string DisplayValue { get; set; }
}
