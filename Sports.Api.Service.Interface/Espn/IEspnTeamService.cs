namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Espn;

public interface IEspnTeamService : IEspnCacheItemService<EspnTeam>
{
    Task<EspnTeam?> Get(
        EspnCompetition competition,
        EspnCompetitor competitor);
}
