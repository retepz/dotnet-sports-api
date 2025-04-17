namespace Sports.Api.Web.Response;

using Sports.Api.Model;

public sealed record GetSportLeaguesResponse
{
    public required SportLeague[] Leagues { get; init; }
}
