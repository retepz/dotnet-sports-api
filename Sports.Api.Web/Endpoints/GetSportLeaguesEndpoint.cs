namespace Sports.Api.Web.Endpoints;

using FastEndpoints;
using Sports.Api.Web.Request;
using Sports.Api.Web.Response;
using Sports.Api.Service.Interface;

public class GetSportLeaguesEndpoint(IGetSportLeagueService getSportLeagueService)
    : Endpoint<GetSportLeaguesRequest, GetSportLeaguesResponse?>
{
    public override void Configure()
    {
        Get("/api/sports/{sportType}/leagues");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSportLeaguesRequest req, CancellationToken ct)
    {
        var leagues = await getSportLeagueService.GetLeagues(req.SportType);
        if (leagues == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = new GetSportLeaguesResponse 
        {
            Leagues = leagues
        };
    }
}
