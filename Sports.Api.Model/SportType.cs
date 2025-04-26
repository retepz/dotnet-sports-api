namespace Sports.Api.Model;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

[JsonConverter(typeof(StringEnumConverter))]
public enum SportType
{
    None = 0,
    Football = 1,
    Basketball = 2,
    Hockey = 3,
    Baseball = 4
}
