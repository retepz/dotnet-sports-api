namespace Sports.Api.Service.Espn;

using Sports.Api.Model;
using Sports.Api.Model.Espn;
using Sports.Api.Model.Interface.Espn;
using Sports.Api.Service.Interface;
using Sports.Api.Service.Interface.Espn;
using System.Collections.Concurrent;

public class EspnEventService(
    IEspnCompetitionService espnCompetitionService,
    ICacheService cacheService,
    IEspnApiService<EspnEvent> espnApiService)
    : EspnCacheItemService<EspnEvent>(cacheService, espnApiService), IEspnEventService
{
    public async Task<IEnumerable<EspnEvent>> Get(
     LeagueType leagueType,
     IEspnEventCollection eventCollection)
    {
        var results = new ConcurrentBag<EspnEvent>();

        await Parallel.ForEachAsync(eventCollection.EventUrls, async (item, _cancelToken) =>
        {
            var espnEvent = await Get(item);
            if (espnEvent == null)
            {
                return;
            }

            espnEvent.LeagueType = leagueType;
            espnEvent.CurrentCompetitions = await espnCompetitionService.Get(espnEvent);

            if (!IsValid(espnEvent))
            {
                return;
            }

            results.Add(espnEvent);
        });

        return GetOrderedEvents(results);
    }

    private bool IsValid(EspnEvent espnEvent)
    {
        return EspnValidator.IdIsValid(espnEvent)
            && espnEvent.CurrentCompetitions != null && espnEvent.CurrentCompetitions.Any();
    }

    private IEnumerable<EspnEvent> GetOrderedEvents(IEnumerable<EspnEvent> events)
    {
        return events
            .OrderBy(nc => nc.Competition.Date)
            .ThenBy(nc => nc.Competition.FirstCompetitor.CurrentTeam.ShortDisplayName);
    }
}
