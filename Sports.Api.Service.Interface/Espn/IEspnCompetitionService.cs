namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Espn;

public interface IEspnCompetitionService : IEspnCacheItemService<EspnCompetition>
{
    Task<IEnumerable<EspnCompetition>> Get(EspnEvent espnEvent);
}
