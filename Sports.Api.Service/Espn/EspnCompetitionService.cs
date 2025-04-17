namespace Sports.Api.Service.Espn;

using Sports.Api.Service.Interface;
using System.Collections.Concurrent;
using Sports.Api.Model.Espn;
using Sports.Api.Service.Interface.Espn;

public class EspnCompetitionService(
    IEspnCacheItemService<EspnBroadcast> broadcastService,
    IEspnCacheItemService<EspnStatus> statusService,
    IEspnCompetitorsService competitorsService,
    IEspnCacheItemService<EspnSituation> situationCacheService,
    IEspnApiService<EspnSituation> situationApiService,
    ICacheService cacheService,
    IEspnApiService<EspnCompetition> espnApiService)
    : EspnCacheItemService<EspnCompetition>(cacheService, espnApiService), IEspnCompetitionService
{
    public async Task<IEnumerable<EspnCompetition>> Get(EspnEvent espnEvent)
    {
        var results = new ConcurrentBag<EspnCompetition>();
        await Parallel.ForEachAsync(espnEvent.CompetitionUrls, async (compUrl, _cancelToken) =>
        {
            var competition = await Get(compUrl);
            if (competition == null)
            {
                return;
            }

            await SetCompetitionValues(competition);

            if (!IsValid(competition))
            {
                return;
            }

            results.Add(competition);
        });

        return results;
    }
    private async Task SetCompetitionValues(EspnCompetition competition)
    {
        // Check status first because multiple things depend on it for cache purposes.
        competition.CurrentStatus = await statusService.Get(competition.StatusUrl);
        competition.CurrentBroadcast = await broadcastService.Get(competition.BroadcastUrl);
        competition.CurrentCompetitors = await competitorsService.Get(competition);
        competition.CurrentSituation = await GetSituation(competition);
    }

    private async Task<EspnSituation?> GetSituation(EspnCompetition competition)
    {
        if (competition.SituationUrl == null)
        {
            return null;
        }

        if (competition.CurrentStatus.IsInProgress)
        {
            return await situationApiService.Get(competition.SituationUrl);
        }

        return await situationCacheService.Get(competition.SituationUrl);
    }

    private bool IsValid(EspnCompetition espnCompetition)
    {
        return EspnValidator.IdIsValid(espnCompetition)
            && espnCompetition.CurrentCompetitors != null && espnCompetition.CurrentCompetitors.Any();
    }
}
