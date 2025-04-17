namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model;
using Sports.Api.Model.Espn;

public interface IEspnSportService
{
    Task<EspnSport?> Get(SportType sportType);
    Task<IList<EspnLeague>?> GetLeagues(EspnSport espnSport);
}
