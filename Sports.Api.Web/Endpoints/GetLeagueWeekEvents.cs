namespace Sports.Api.Web.Endpoints;

using FastEndpoints;
using Sports.Api.Web.Request;
using Sports.Api.Model;
using Sports.Api.Service.Interface;

public class GetLeagueWeekEvents(IGetSportLeagueService getSportLeagueService)
    : Endpoint<GetLeagueWeekEventsRequest, SportLeagueEvent[]?>
{
    public override void Configure()
    {
        Get("/api/leagues/{leagueType}/season/currentweek/events");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLeagueWeekEventsRequest req, CancellationToken ct)
    {
        var events = await getSportLeagueService.GetWeekEvents(req.LeagueType);
        if(events == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = events;
    }
}
