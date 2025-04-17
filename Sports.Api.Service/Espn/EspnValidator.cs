namespace Sports.Api.Service.Espn;

using Sports.Api.Model.Interface.Espn;

public static class EspnValidator
{
    public static bool IdIsValid(IEspnId espnObject)
    {
        if (int.TryParse(espnObject.Id, out var id))
        {
            return id > 0;
        }

        return false;
    }
}
