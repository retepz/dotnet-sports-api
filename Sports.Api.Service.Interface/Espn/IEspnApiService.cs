namespace Sports.Api.Service.Interface.Espn;

using Sports.Api.Model.Interface.Espn;
using Sports.Api.Model.Espn;

public interface IEspnApiService<T>
    where T : class, IEspnApiUrl
{
    Task<T?> Get(EspnApiUrl apiUrl);
}
