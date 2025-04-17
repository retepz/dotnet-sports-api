namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnApiUrl : IEspnApiUrl
{
    private string? _url;

    [JsonProperty("$ref")]
    public string Url
    {
        get => _url!;
        set
        {
            if (value == null)
            {
                return;
            }

            if (value!.StartsWith("https"))
            {
                _url = value;
                return;
            }

            _url = value.Replace("http", "https");
        }
    }
}
