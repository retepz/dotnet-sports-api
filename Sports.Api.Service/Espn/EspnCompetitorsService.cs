namespace Sports.Api.Service.Espn;

using Sports.Api.Service.Interface;
using System.Collections.Concurrent;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface.Espn;

public class EspnCompetitorsService(
    IEspnTeamService espnTeamService,
    IEspnCacheItemService<EspnScore> espnScoreCacheService,
    IEspnApiService<EspnScore> espnScoreApiService,
    IEspnApiService<EspnCompetitor> espnApiService,
    ICacheService cacheService) : EspnCacheItemService<EspnCompetitor>(cacheService, espnApiService), IEspnCompetitorsService
{
    public async Task<IEnumerable<EspnCompetitor>?> Get(EspnCompetition competition)
    {
        var results = new ConcurrentBag<EspnCompetitor>();

        await Parallel.ForEachAsync(competition.CompetitorUrls, async (url, _cancelToken) =>
        {
            var competitor = await Get(url);
            if (competition == null || competitor == null || !EspnValidator.IdIsValid(competitor))
            {
                return;
            }

            var currentTeam = await espnTeamService.Get(competition, competitor);
            if (currentTeam == null)
            {
                return;
            }

            competitor.CurrentTeam = currentTeam;
            competitor.CurrentScore = (await GetCurrentScore(competition, competitor))!;

            results.Add(competitor);
        });

        return !results.IsEmpty ? results : null;
    }

    private async Task<EspnScore?> GetCurrentScore(EspnCompetition competition, EspnCompetitor competitor)
    {
        if (competition.CurrentStatus.IsInProgress)
        {
            return await espnScoreApiService.Get(competitor.ScoreUrl);
        }

        return await espnScoreCacheService.Get(competitor.ScoreUrl);
    }
}
