class CalendarConverter {
    public static Dictionary<string, CalendarInfo> calendars = new() {
        {
            "nyc",
            new CalendarInfo(
                calendarName: "Nicer Years Calendar",
                monthNames: new List<string> { "Ūnum", "Duōs", "Trēs", "Quattor", "Quīnque", "Sex", "Septem", "Octō", "Novem", "Decem", "Ūndecim", "Duodecim" },
                monthDurations: new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 },
                leapDays: new Dictionary<int, int> { { 2, 1 } },
                gregorianStartDay: 1,
                gregorianStartMonth: 1,
                gregorianYearOffset: 73
            )
        },
        {
            "mc",
            new CalendarInfo(
                calendarName: "Millennium Calendar",
                monthNames: new List<string> { "Ūnum", "Duōs", "Trēs", "Quattor", "Quīnque", "Sex", "Septem", "Octō", "Novem", "Decem", "Ūndecim", "Duodecim" },
                monthDurations: new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 },
                leapDays: new Dictionary<int, int> { { 2, 1 } },
                gregorianStartDay: 1,
                gregorianStartMonth: 1,
                gregorianYearOffset: 960
            )
        },
        {
            "rc",
            new CalendarInfo(
                calendarName: "Retrollennium Calendar",
                monthNames: new List<string> { "Ūnum", "Duōs", "Trēs", "Quattor", "Quīnque", "Sex", "Septem", "Octō", "Novem", "Decem", "Ūndecim", "Duodecim" },
                monthDurations: new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 },
                leapDays: new Dictionary<int, int> { { 2, 1 } },
                gregorianStartDay: 1,
                gregorianStartMonth: 1,
                gregorianYearOffset: -960
            )
        }
    };

    static void Main(string[] args) {
        Console.WriteLine("Enter a date (YYYY-MM-DD):");
        string? inputDate = Console.ReadLine();

        if (!DateTime.TryParse(inputDate, out DateTime gregorianDate)) {
            Console.WriteLine($"Invalid date format. Use YYYY-MM-DD next time. Falling back to today's date ({DateTime.Today.ToString("yyyy-MM-dd")}) for now.");
            gregorianDate = DateTime.Today;
        }

        Console.WriteLine("Accepted Calendar IDs: gregorian   nyc   mc   rc   all");
        Console.WriteLine("Enter calendar ID:");
        string? calendarIdInput = Console.ReadLine()?.Trim()?.ToLower() ?? "gregorian";
        string calendarId = string.IsNullOrEmpty(calendarIdInput) ? "gregorian" : calendarIdInput;

        string converted = ConvertToCalendar(gregorianDate, calendarId);

        Console.WriteLine($"Gregorian {gregorianDate:yyyy-MM-dd} in {calendarId} calendar, alongside other information...:\n{converted}");
    }

    static string ConvertToCalendar(DateTime date, string calendarId) {
        switch (calendarId) {
            case "gregorian":
                return $"{date.Day:D2}.{date.Month:D2}.{date.Year}";

            case "nyc":
                return ConvertTo_NYC(date);

            case "mc":
                return ConvertTo_MC(date);

            case "rc":
                return ConvertTo_RC(date);

            case "all":
                return ConvertToAllCalendars(date);

            default:
                return "Unknown calendar ID.";
        }
    }

    static string ConvertToAllCalendars(DateTime date) {
        string all_calendars = "All Calendars:\n";
        all_calendars += $"{calendars["nyc"].calendarName}: {ConvertTo_NYC(date)}\n";
        all_calendars += $"{calendars["mc"].calendarName}: {ConvertTo_MC(date)}\n";
        all_calendars += $"{calendars["rc"].calendarName}: {ConvertTo_RC(date)}\n";
        return all_calendars;
    }

    static string ConvertTo_NYC(DateTime date) {
        int year = date.Year + 73;
        string month_word = calendars["nyc"].monthNames[date.Month-1];

        return $"Date: {date.Day:D2}.{date.Month:D2}.{year} /// {date.Day:D2}.{date.Month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }

    static string ConvertTo_MC(DateTime date) {
        int year = date.Year + 960;
        string month_word = calendars["mc"].monthNames[date.Month-1];

        return $"Date: {date.Day:D2}.{date.Month:D2}.{year} /// {date.Day:D2}.{date.Month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }

    static string ConvertTo_RC(DateTime date) {
        int year = date.Year - 30;
        string month_word = calendars["rc"].monthNames[date.Month-1];

        return $"Date: {date.Day:D2}.{date.Month:D2}.{year} /// {date.Day:D2}.{date.Month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }
}
