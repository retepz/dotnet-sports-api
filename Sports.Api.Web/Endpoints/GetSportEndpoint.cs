namespace Sports.Api.Web.Endpoints;

using FastEndpoints;
using Sports.Api.Web.Request;
using Sports.Api.Model;
using Sports.Api.Service.Interface;

public class GetSportEndpoint(IGetSportService getSportService)
    : Endpoint<GetSportRequest, Sport?>
{
    public override void Configure()
    {
        Get("/api/sports/{sportType}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSportRequest req, CancellationToken ct)
    {
        var sport = await getSportService.Get(req.SportType);
        if (sport == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = sport;
    }
}
