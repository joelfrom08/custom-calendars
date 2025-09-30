class MainProgram {
    int consoleWidth = 0;
    int consoleHeight = 0;
    
    static void Main(string[] args) {
        Console.CursorVisible = false;
        var program = new MainProgram();
        Task.Run(() => program.CheckForResize());

        Console.SetCursorPosition(0, 2);
        // Console.WriteLine("Enter a date (YYYY-MM-DD):");
        string? inputDate = Console.ReadLine();

        if (!DateTime.TryParse(inputDate, out DateTime gregorianDate)) {
            Console.WriteLine($"Invalid date format. Use YYYY-MM-DD next time. Falling back to today's date ({DateTime.Today.ToString("yyyy-MM-dd")}) for now.");
            gregorianDate = DateTime.Today;
        }

        Console.WriteLine("Accepted Calendar IDs: gregorian | jc | joc | nyc | juc | mc | opc | omc | rc | gtc | all");
        Console.WriteLine("Enter calendar ID:");
        string? calendarIdInput = Console.ReadLine()?.Trim()?.ToLower() ?? "gregorian";
        string calendarId = string.IsNullOrEmpty(calendarIdInput) ? "gregorian" : calendarIdInput;

        string converted = ConvertToCalendar(gregorianDate, calendarId);

        /// Console.WriteLine($"Gregorian {gregorianDate:yyyy-MM-dd} in {calendarId} calendar, alongside other information...:\n{converted}");
        Console.WriteLine(converted);
    }

    void CheckForResize() {
        while (true) {
            if (Console.WindowWidth != consoleWidth || Console.WindowHeight != consoleHeight) {
                MenuManager.DrawMenuBackground();
                MenuManager.CalculateBoundaries();
                MenuManager.CalculateTitleVersionGradient();
                MenuManager.DrawTitle();
                MenuManager.DrawVersion();
                MenuManager.DrawCopyright();
                MenuManager.DrawWindow(20, 6);

                consoleWidth = Console.WindowWidth;
                consoleHeight = Console.WindowHeight;
            }
            Thread.Sleep(1);
        }
    }


    static string ConvertToCalendar(DateTime date, string calendarId) {
        switch (calendarId) {
            case "gregorian":
                return $"{date.Day:D2}.{date.Month:D2}.{date.Year}";

            case "jc":
                return CalendarConversion.ConvertTo_JC(date);

            case "joc":
                return CalendarConversion.ConvertTo_JOC(date);

            case "nyc":
                return CalendarConversion.ConvertTo_NYC(date);

            case "juc":
                return CalendarConversion.ConvertTo_JUC(date);

            case "mc":
                return CalendarConversion.ConvertTo_MC(date);

            case "opc":
                return CalendarConversion.ConvertTo_OPC(date);

            case "omc":
                return CalendarConversion.ConvertTo_OMC(date);

            case "rc":
                return CalendarConversion.ConvertTo_RC(date);

            case "gtc":
                return CalendarConversion.ConvertTo_GTC(date);

            case "all":
                return CalendarConversion.ConvertToAllCalendars(date);

            default:
                return "Unknown calendar ID.";
        }
    }
}