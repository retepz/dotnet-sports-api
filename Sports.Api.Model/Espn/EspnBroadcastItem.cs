namespace Sports.Api.Model.Espn;

public class EspnBroadcastItem
{
    public string Station { get; set; }
    public string Slug { get; set; }
    public EspnBroadcastMarket Market { get; set; }
    public EspnBroadcastMedia Media { get; set; }
}
