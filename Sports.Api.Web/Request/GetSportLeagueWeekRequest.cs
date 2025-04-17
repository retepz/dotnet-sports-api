namespace Sports.Api.Web.Request;

using Sports.Api.Model;

public sealed record GetSportLeagueWeekRequest
{
    public LeagueType LeagueType { get; init; }
}
