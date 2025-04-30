namespace Sports.Api.Web.Request;

using Sports.Api.Model;

public sealed record GetLeagueWeekEventsRequest
{
    public LeagueType LeagueType { get; init; }
}
