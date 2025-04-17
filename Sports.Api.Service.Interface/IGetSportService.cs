namespace Sports.Api.Service.Interface;

using Sports.Api.Model;

public interface IGetSportService
{
    Task<Sport?> Get(SportType sportType);
}
