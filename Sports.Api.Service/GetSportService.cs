namespace Sports.Api.Service;

using Sports.Api.Model;
using Sports.Api.Service.Interface;
using Sports.Api.Service.Interface.Espn;

public sealed class GetSportService(
    IEspnSportService espnSportService)
    : IGetSportService
{
    public async Task<Sport?> Get(SportType sportType) 
    {
        var espnSport = await espnSportService.Get(sportType);
        if (espnSport == null)
        {
            return null;
        }

        return new(espnSport);
    }
}
