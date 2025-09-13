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
                gregorianYearOffset: 73,
                leapDaysCalculator: year => ((year-73) % 4 == 0 && ((year-73) % 100 != 0 || (year-73) % 400 == 0)) ? true : false
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
                gregorianYearOffset: 960,
                leapDaysCalculator: year => ((year-960) % 4 == 0 && ((year-960) % 100 != 0 || (year-960) % 400 == 0)) ? true : false
            )
        },
        {
            "omc",
            new CalendarInfo(
                calendarName: "OmCal",
                monthNames: new List<string> { "Etra", "\u01b7wotra", "Tretra", "Ohf" },
                monthDurations: new List<int> { 119, 119, 119, 8 },
                leapDays: new Dictionary<int, int> { { 3, 1 } },
                gregorianStartDay: 15,
                gregorianStartMonth: 6,
                gregorianYearOffset: -1953,
                leapDaysCalculator: year => ((year-46) % 4 == 0 && ((year-46) % 100 != 0 || (year-46) % 400 == 0)) ? true : false
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
                gregorianYearOffset: -30,
                leapDaysCalculator: year => ((year+30) % 4 == 0 && ((year+30) % 100 != 0 || (year+30) % 400 == 0)) ? true : false
            )
        },
        {
            "gtc",
            new CalendarInfo(
                calendarName: "GoodTimes Calendar",
                monthNames: new List<string> { "Einn", "Dotum", "Trettium", "Quadrum", "Femta", "Sektum", "Septum", "Oktum", "Novum", "Tetum", "Ävum", "Tvovum" },
                monthDurations: new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 },
                leapDays: new Dictionary<int, int> { { 2, 1 } },
                gregorianStartDay: 21,
                gregorianStartMonth: 3,
                gregorianYearOffset: -16,
                leapDaysCalculator: year => ((year+16) % 4 == 0 && ((year+16) % 100 != 0 || (year+16) % 400 == 0)) ? true : false
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

        Console.WriteLine("Accepted Calendar IDs: gregorian   nyc   mc   omc   rc   gtc   all");
        Console.WriteLine("Enter calendar ID:");
        string? calendarIdInput = Console.ReadLine()?.Trim()?.ToLower() ?? "gregorian";
        string calendarId = string.IsNullOrEmpty(calendarIdInput) ? "gregorian" : calendarIdInput;

        string converted = ConvertToCalendar(gregorianDate, calendarId);

        /// Console.WriteLine($"Gregorian {gregorianDate:yyyy-MM-dd} in {calendarId} calendar, alongside other information...:\n{converted}");
        Console.WriteLine(converted);
    }

    static string ConvertToCalendar(DateTime date, string calendarId) {
        switch (calendarId) {
            case "gregorian":
                return $"{date.Day:D2}.{date.Month:D2}.{date.Year}";

            case "nyc":
                return ConvertTo_NYC(date);

            case "mc":
                return ConvertTo_MC(date);

            case "omc":
                return ConvertTo_OMC(date);

            case "rc":
                return ConvertTo_RC(date);

            case "gtc":
                return ConvertTo_GTC(date);

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
        all_calendars += $"{calendars["omc"].calendarName}: {ConvertTo_OMC(date)}\n";
        all_calendars += $"{calendars["rc"].calendarName}: {ConvertTo_RC(date)}\n";
        all_calendars += $"{calendars["gtc"].calendarName}: {ConvertTo_GTC(date)}\n";
        return all_calendars;
    }

    static string ConvertTo_NYC(DateTime date) {
        int year = date.Year + calendars["nyc"].gregorianYearOffset;
        string month_word = calendars["nyc"].monthNames[date.Month - 1];

        return $"Date: {date.Day:D2}.{date.Month:D2}.{year} /// {date.Day:D2}.{date.Month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }

    static string ConvertTo_MC(DateTime date) {
        int year = date.Year + calendars["mc"].gregorianYearOffset;
        string month_word = calendars["mc"].monthNames[date.Month - 1];

        return $"Date: {date.Day:D2}.{date.Month:D2}.{year} /// {date.Day:D2}.{date.Month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }
    
    static string ConvertTo_OMC(DateTime date) {
        /// Initialize month and day values to start on 01.01.; Additionally, make a copy of monthDurations for later use
        int month = 1;
        int day = 1;
        List<int> adjustedMonthDurations = calendars["omc"].monthDurations;

        // Calculate year using current gregorian year ± the offset of the calendar. If the calendar's new year has yet to pass, reduce the year by one. (2025-06-15 → 01.01.72[0]; 2025-06-14 → 08.04.71[0])
        int year = date.Year + calendars["omc"].gregorianYearOffset;
        if (date.Month < calendars["omc"].gregorianStartDate.Month || (date.Month == calendars["omc"].gregorianStartDate.Month && date.Day < calendars["omc"].gregorianStartDate.Day)) year--;
        int displayedYear = year;

        /// Calculate century from the year
        int century = (int)MathF.Floor(year / 100);

        /// Weird hack: If year is negative and not a multiple of 100, add 100 to displayedYear until it is >= 0, and subtract 1 from century.
        if (year < 0 && year % 100 != 0) {
            century--;
            while (displayedYear < 0) {
                displayedYear += 100;
            }
        }

        /// Calculate days in the custom year using the sum of all monthDurations. If this is a leap year, add all extra days from leapDays.
        int days_in_omc_year = calendars["omc"].monthDurations.Sum();
        if (calendars["omc"].leapDaysCalculator(year)) {
            days_in_omc_year += calendars["omc"].leapDays.Values.Sum();
            foreach (KeyValuePair<int, int> leapDay in calendars["omc"].leapDays) {
                adjustedMonthDurations[leapDay.Key-1] += leapDay.Value;
            }
        }

        /// Calculate the amount of days since new year using the total amount of days since current_gregorian_year-start_month_start_day. If this is a negative number, add on the amount of total days in a year to get a positive.
        double days_since_new_year = (date - new DateTime(date.Year, calendars["omc"].gregorianStartDate.Month, calendars["omc"].gregorianStartDate.Day)).TotalDays;
        if (days_since_new_year < 0) days_since_new_year += days_in_omc_year;

        /// Set day to days_since_new_year+1, and gradually reduce it down to a realistic day until the current month is yet to be over.
        day = (int)days_since_new_year + 1;
        foreach (int monthD in adjustedMonthDurations) {
            if (monthD >= day) { break; }
            day -= monthD;
            month++;
        }
        string month_word = calendars["omc"].monthNames[month-1];

        return $"Date: {day:D2}.{month:D2}.{(displayedYear % 100):D2}[{century}] /// {day:D2}.{month:D2}.{(displayedYear % 100):D2},{century}\nMonth: {month_word}";
    }

    static string ConvertTo_RC(DateTime date) {
        int year = date.Year + calendars["rc"].gregorianYearOffset;
        string month_word = calendars["rc"].monthNames[date.Month - 1];

        return $"Date: {date.Day:D2}.{date.Month:D2}.{year} /// {date.Day:D2}.{date.Month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }

    static string ConvertTo_GTC(DateTime date) {
        /// Initialize month and day values to start on 01.01.; Additionally, make a copy of monthDurations for later use
        int month = 1;
        int day = 1;
        List<int> adjustedMonthDurations = calendars["gtc"].monthDurations;

        /// Calculate year using current gregorian year ± the offset of the calendar. If the calendar's new year has yet to pass, reduce the year by one. (2025-03-21 → 01.01.2009; 2025-03-20 → 31.12.2008)
        int year = date.Year + calendars["gtc"].gregorianYearOffset;
        if (date.Month < calendars["gtc"].gregorianStartDate.Month || (date.Month == calendars["gtc"].gregorianStartDate.Month && date.Day < calendars["gtc"].gregorianStartDate.Day)) year--;

        /// Calculate days in the custom year using the sum of all monthDurations. If this is a leap year, add all extra days from leapDays.
        int days_in_gtc_year = calendars["gtc"].monthDurations.Sum();
        if (calendars["gtc"].leapDaysCalculator(year)) {
            days_in_gtc_year += calendars["gtc"].leapDays.Values.Sum();
            foreach (KeyValuePair<int, int> leapDay in calendars["gtc"].leapDays) {
                adjustedMonthDurations[leapDay.Key-1] += leapDay.Value;
            }
        }

        /// Calculate the amount of days since new year using the total amount of days since current_gregorian_year-start_month_start_day. If this is a negative number, add on the amount of total days in a year to get a positive.
        double days_since_new_year = (date - new DateTime(date.Year, calendars["gtc"].gregorianStartDate.Month, calendars["gtc"].gregorianStartDate.Day)).TotalDays;
        if (days_since_new_year < 0) days_since_new_year += days_in_gtc_year;

        /// Set day to days_since_new_year+1, and gradually reduce it down to a realistic day until the current month is yet to be over.
        day = (int)days_since_new_year + 1;
        foreach (int monthD in adjustedMonthDurations) {
            if (monthD >= day) { break; }
            day -= monthD;
            month++;
        }
        string month_word = calendars["gtc"].monthNames[month-1];
        
        return $"Date: {day:D2}.{month:D2}.{year} /// {day:D2}.{month:D2}.{(year % 100):D2}\nMonth: {month_word}";
    }
}