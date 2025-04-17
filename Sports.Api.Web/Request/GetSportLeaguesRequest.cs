namespace Sports.Api.Web.Request;

using Sports.Api.Model;

public sealed record GetSportLeaguesRequest
{
    public SportType SportType { get; init; }
}
