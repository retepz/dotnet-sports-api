namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnApiLogo
{
    [JsonProperty("href")]
    public string Url { get; set; }

    [JsonProperty("rel")]
    public string[] MetaData { get; set; }

    [JsonIgnore]
    public bool IsDark => MetaData.Any(md => md.Contains("dark", StringComparison.OrdinalIgnoreCase));

    [JsonIgnore]
    public bool IsScoreboard => MetaData.Any(md => md.Contains("scoreboard", StringComparison.OrdinalIgnoreCase));
}
