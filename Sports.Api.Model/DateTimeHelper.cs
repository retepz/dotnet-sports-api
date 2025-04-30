namespace Sports.Api.Model;

using System;

public static class DateTimeHelper
{
    public static string? GetGameDateDisplay(DateTime? dateTime)
    {
        return dateTime?.ToString("ddd MMM d");
    }

    public static string? GameTimeDisplay(DateTime? dateTime)
    {
        return dateTime?.ToString("h:mm tt");
    }

    public static string WeekStartEndDisplay(DateTime start, DateTime end)
    {
        var startDisplay = start.ToString("MMM d");
        var endDisplay = end.ToString("MMM d");
        return $"{startDisplay} - {endDisplay}";
    }
}