namespace Sports.Api.Model.Interface.Espn;

using Sports.Api.Model.Espn;
using System.Collections.Concurrent;

public interface IEspnEventCollection
{
    ConcurrentQueue<EspnApiUrl> EventUrls { get; set; }
    int EventCount { get; set; }
    int PageCount { get; set; }
    int PageSize { get; set; }
    int PageIndex { get; set; }
}
