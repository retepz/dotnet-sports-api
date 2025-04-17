namespace Sports.Api.Model;

using Sports.Api.Model.Espn;

public sealed record Sport
{
    public Sport(EspnSport espnSport)
    {
        Id = espnSport.Id;
        DisplayName = espnSport.DisplayName;
        Logos = espnSport
            .Logos?
            .Select(l => l.Url)
            .ToArray();
    }

    public string Id { get; }

    public string DisplayName { get; }

    public string[]? Logos { get; }
}
