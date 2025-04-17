namespace Sports.Api.Service.Espn;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Sports.Api.Service.Interface.Espn;
using Sports.Api.Model.Interface.Espn;
using Sports.Api.Model.Espn;

public class EspnApiService<T> : IEspnApiService<T>
    where T : class, IEspnApiUrl
{
    public const string EventsPath = "events";

    public async Task<T?> Get(EspnApiUrl apiUrl)
    {
        using var client = new HttpClient();
        try
        {
            var result = await client.GetAsync(apiUrl.Url);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var asString = await result.Content.ReadAsStringAsync();

            DefaultContractResolver contractResolver = new()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var deserializedResult = JsonConvert.DeserializeObject<T>(asString, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore
            })!;

            if (string.IsNullOrEmpty(deserializedResult.Url))
            {
                deserializedResult.Url = apiUrl.Url;
            }

            return deserializedResult;
        }
        catch
        {
            return null;
        }
    }
}
