namespace Sports.Api.Web.Endpoints;

using FastEndpoints;
using Sports.Api.Web.Request;
using Sports.Api.Model;
using Sports.Api.Service.Interface;

public class GetLeagueWeekEndpoint(IGetSportLeagueService getSportLeagueService)
    : Endpoint<GetSportLeagueWeekRequest, SportLeagueWeek?>
{
    public override void Configure()
    {
        Get("/api/leagues/{leagueType}/season/currentweek");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSportLeagueWeekRequest req, CancellationToken ct)
    {
        var week = await getSportLeagueService.GetCurrentWeek(req.LeagueType);
        if(week == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = week;
    }
}
