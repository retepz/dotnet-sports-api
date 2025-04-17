namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;
using Sports.Api.Model.Interface.Espn;

public class EspnCompetition : EspnCacheItem, IEspnId
{
    [JsonProperty("competitors")]
    public EspnApiUrl[] CompetitorUrls { get; set; }

    public string Id { get; set; }

    [JsonProperty("status")]
    public EspnApiUrl StatusUrl { get; set; }

    [JsonProperty("broadcasts")]
    public EspnApiUrl BroadcastUrl { get; set; }

    [JsonProperty("Situation")]
    public EspnApiUrl SituationUrl { get; set; }

    public DateTime Date { get; set; }
    public int Attendance { get; set; }

    [JsonIgnore]
    public EspnStatus CurrentStatus { get; set; }

    [JsonIgnore]
    public EspnBroadcast CurrentBroadcast { get; set; }

    [JsonIgnore]
    public EspnSituation CurrentSituation { get; set; }

    [JsonIgnore]
    public IEnumerable<EspnCompetitor> CurrentCompetitors { get; set; }

    [JsonProperty("type")]
    public EspnApiCompetitionType CurrentType { get; set; }

    [JsonIgnore]
    public EspnCompetitor? CompetitorWithPosession
    {
        get
        {
            var gameInProgress = CurrentStatus?.IsInProgress ?? false;
            if (FirstCompetitor == null || CurrentSituation?.CurrentTeamPossessionUrl?.Url == null || !gameInProgress)
            {
                return null;
            }

            var firstTeamUrl = new Uri(FirstCompetitor.TeamUrl.Url).AbsolutePath;
            var currentSituationUrl = new Uri(CurrentSituation.CurrentTeamPossessionUrl.Url).AbsolutePath;
            var isFirstCompetitor = firstTeamUrl == currentSituationUrl;

            return isFirstCompetitor ? FirstCompetitor : SecondCompetitior;
        }
    }


    [JsonIgnore]
    public EspnCompetitor FirstCompetitor => CurrentCompetitors.First(c => c.IsHome);

    [JsonIgnore]
    public EspnCompetitor SecondCompetitior => CurrentCompetitors.First(c => !c.IsHome);

    public override bool CacheNeverExpires => true;

}
