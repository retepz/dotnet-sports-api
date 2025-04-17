namespace Sports.Api.Web.Request;

using Sports.Api.Model;

public sealed record GetSportRequest
{
    public SportType SportType { get; init; }
}
