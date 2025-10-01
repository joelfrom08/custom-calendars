namespace PetByte.CustomCalendars {
    static class MainProgram {
        static int consoleWidth = 0;
        static int consoleHeight = 0;

        static void Main(string[] args) {
            Console.CursorVisible = false;
            Task.Run(() => CheckForResize());

            Console.SetCursorPosition(0, 2);
            string? inputDate = Console.ReadLine();

            MenuManager.DrawWindow("calendar_input");

            if (!DateTime.TryParse(inputDate, out DateTime gregorianDate)) { gregorianDate = DateTime.Today; }

            Console.WriteLine("Accepted Calendar IDs: gregorian | jc | joc | nyc | juc | mc | opc | omc | rc | gtc | all");
            Console.WriteLine("Enter calendar ID:");
            string? calendarIdInput = Console.ReadLine()?.Trim()?.ToLower() ?? "gregorian";
            string calendarId = string.IsNullOrEmpty(calendarIdInput) ? "gregorian" : calendarIdInput;

            string converted = ConvertToCalendar(gregorianDate, calendarId);

            /// Console.WriteLine($"Gregorian {gregorianDate:yyyy-MM-dd} in {calendarId} calendar, alongside other information...:\n{converted}");
            Console.WriteLine(converted);
        }

        static void CheckForResize() {
            while (true) {
                if (Console.WindowWidth != consoleWidth || Console.WindowHeight != consoleHeight) {
                    Console.Clear();
                    MenuManager.DrawMenuBackground();
                    MenuManager.CalculateBoundaries();
                    MenuManager.CalculateTitleVersionGradient();
                    MenuManager.DrawTitle();
                    MenuManager.DrawVersion();
                    MenuManager.DrawCopyright();
                    MenuManager.DrawWindow(MenuManager.currentWindow);

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
}