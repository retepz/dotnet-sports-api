namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnStatusType
{
    public bool Completed { get; set; }
    public string Name { get; set; }

    public string ShortDetail { get; set; }

    [JsonIgnore]
    public EspnStatusTypeName CurrentName
    {
        get
        {
            if (Enum.TryParse<EspnStatusTypeName>(Name, out var typeName))
            {
                return typeName;
            }

            return EspnStatusTypeName.NONE;
        }
    }
}
