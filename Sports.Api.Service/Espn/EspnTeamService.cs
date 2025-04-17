namespace Sports.Api.Service.Espn;

using Sports.Api.Service.Interface;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface.Espn;

public class EspnTeamService(
    ICacheService cacheService,
    IEspnApiService<EspnTeam> espnApiService,
    IEspnApiService<EspnTeamRecord> espnTeamRecordApiService,
    IEspnCacheItemService<EspnTeamRecord> espnTeamRecordCacheService)
    : EspnCacheItemService<EspnTeam>(cacheService, espnApiService), IEspnTeamService
{
    public async Task<EspnTeam?> Get(
        EspnCompetition competition,
        EspnCompetitor competitor)
    {
        var team = await Get(competitor.TeamUrl);
        if (team == null || !EspnValidator.IdIsValid(team))
        {
            return null;
        }

        team.CurrentRecord = await GetRecord(competition, team);

        return team;
    }

    public async Task<EspnTeamRecord?> GetRecord(EspnCompetition competition, EspnTeam team)
    {
        if (competition.CurrentStatus.IsInProgress)
        {
            return await espnTeamRecordApiService.Get(team.RecordUrl);
        }

        return await espnTeamRecordCacheService.Get(team.RecordUrl);
    }
}
