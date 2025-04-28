namespace Sports.Api.Model;

using Sports.Api.Model.Espn;
using System.Text.Json.Serialization;

public sealed record SportLeague
{
    public SportLeague(EspnLeague league)
    {
        Id = league.Id;
        LeagueType = league.LeagueType;
        ShortName = league.ShortName;
        DisplayName = league.DisplayName;
        Name = league.Name;
        Abbreviation = league.Abbreviation;
        Logos = league
            .Logos?
            .Select(l => l.Url)
            .ToArray();
    }

    public string? Id { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]

    public LeagueType LeagueType { get; }

    public string? ShortName { get; }

    public string? DisplayName { get; }

    public string? Name { get; }

    public string? Abbreviation { get; }

    public string[]? Logos { get; }
}
