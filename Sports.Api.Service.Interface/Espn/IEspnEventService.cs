namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Model.Interface.Espn;

public interface IEspnEventService : IEspnCacheItemService<EspnEvent>
{
    Task<IEnumerable<EspnEvent>> Get(
             LeagueType leagueType,
             IEspnEventCollection eventCollection);
}
