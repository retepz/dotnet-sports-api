namespace Sports.Api.Web.Request;

using Sports.Api.Model;

public sealed record GetSportLeagueRequest
{
    public LeagueType LeagueType { get; init; }
}
