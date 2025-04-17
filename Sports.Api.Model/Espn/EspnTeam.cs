namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnTeam : EspnCacheItem, IEspnId
{
    public string Id { get; set; }
    public string Name { get; set; }

    [JsonProperty("displayName")]
    public string FullName { get; set; }
    public string ShortDisplayName { get; set; }
    public string Location { get; set; }
    public string Color { get; set; }
    public string AlternateColor { get; set; }
    public EspnApiLogo[] Logos { get; set; }

    [JsonProperty("record")]
    public EspnApiUrl RecordUrl { get; set; }

    public EspnTeamRecord CurrentRecord { get; set; }

    public override bool CacheNeverExpires => true;

    [JsonIgnore]
    public EspnApiLogo? DefaultLogo
    {
        get
        {
            if (Logos == null || Logos.Length == 0)
            {
                return null;
            }

            return Logos.FirstOrDefault(l => l.IsDark) ?? Logos[0];
        }
    }

    [JsonIgnore]
    public EspnApiLogo? ScoreboardLogo
    {
        get
        {
            if (Logos == null || Logos.Length == 0)
            {
                return null;
            }

            return Logos.FirstOrDefault(l => l.IsDark && l.IsScoreboard);
        }
    }
}
