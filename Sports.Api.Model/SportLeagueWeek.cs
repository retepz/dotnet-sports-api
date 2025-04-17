namespace Sports.Api.Model;

using Sports.Api.Model.Espn;

public sealed record SportLeagueWeek
{
    public SportLeagueWeek(EspnWeek espnWeek) 
    {
        Number = espnWeek.Number;
        StartDate = espnWeek.StartDate;
        EndDate = espnWeek.EndDate;
        DisplayName = espnWeek.DisplayName;
    }

    public int Number { get; }
    public DateTime? StartDate { get; }
    public DateTime? EndDate { get; }
    public string DisplayName { get; }
}
