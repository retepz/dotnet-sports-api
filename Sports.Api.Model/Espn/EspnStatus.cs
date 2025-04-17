namespace Sports.Api.Model.Espn;

using Newtonsoft.Json;

public class EspnStatus : EspnCacheItem
{
    private readonly EspnStatusTypeName[] _inProgressStatuses =
    [
        EspnStatusTypeName.STATUS_IN_PROGRESS,
        EspnStatusTypeName.STATUS_END_PERIOD,
        EspnStatusTypeName.STATUS_HALFTIME
    ];

    [JsonProperty("period")]
    public int CurrentPeriod { get; set; }
    public string DisplayClock { get; set; }

    [JsonProperty("type")]
    public EspnStatusType StatusType { get; set; }

    [JsonIgnore]
    public bool IsCompleted => StatusType.CurrentName == EspnStatusTypeName.STATUS_FINAL;

    [JsonIgnore]
    public bool IsInProgress => _inProgressStatuses.Contains(StatusType.CurrentName);

    [JsonIgnore]
    public bool IsInFuture => StatusType.CurrentName == EspnStatusTypeName.STATUS_SCHEDULED;

    [JsonIgnore]
    public override bool IgnoreCache => IsInFuture || IsInProgress;
}
