namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnApiCompetitionType : EspnCacheItem, IEspnId
{
    public string Id { get; set; }

    [JsonProperty("text")]
    public string DisplayName { get; set; }

    [JsonProperty("abbreviation")]
    public string ShortName { get; set; }

    [JsonProperty("type")]
    public string Name { get; set; }

    public override bool CacheNeverExpires => true;

    [JsonIgnore]
    public EspnCompetitionType CurrentName
    {
        get
        {
            if (Enum.TryParse<EspnCompetitionType>(Name, out var typeName))
            {
                return typeName;
            }

            return EspnCompetitionType.none;
        }
    }
}
