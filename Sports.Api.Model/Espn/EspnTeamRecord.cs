namespace Sports.Api.Model.Espn;

using System.Text.Json.Serialization;

public class EspnTeamRecord : EspnCacheItem
{
    public EspnTeamRecordItem[] Items { get; set; }

    [JsonIgnore]
    public string DisplayValue => Items != null && Items.Length != 0 ? Items[0].DisplayValue : string.Empty;
}
