namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Espn;

public interface IEspnCompetitorsService : IEspnCacheItemService<EspnCompetitor>
{
    Task<IEnumerable<EspnCompetitor>?> Get(EspnCompetition competition);
}
