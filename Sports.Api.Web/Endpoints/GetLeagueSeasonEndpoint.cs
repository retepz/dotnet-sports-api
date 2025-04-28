namespace Sports.Api.Web.Endpoints;

using FastEndpoints;
using Sports.Api.Web.Request;
using Sports.Api.Model;
using Sports.Api.Service.Interface;

public class GetLeagueSeasonEndpoint(IGetSportLeagueService getSportLeagueService)
    : Endpoint<GetSportLeagueRequest, SportLeagueSeason?>
{
    public override void Configure()
    {
        Get("/api/leagues/{leagueType}/season");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSportLeagueRequest req, CancellationToken ct)
    {
        var season = await getSportLeagueService.GetSeason(req.LeagueType);
        if(season == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = season;
    }
}
