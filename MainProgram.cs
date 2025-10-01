namespace PetByte.CustomCalendars {
    static class MainProgram {
        static int consoleWidth = 0;
        static int consoleHeight = 0;

        public static DateTime properInputDate;

        static void Main(string[] args) {
            Console.CursorVisible = false;
            Task.Run(() => CheckForResize());

            Console.SetCursorPosition(0, 2);
            string? inputDate = Console.ReadLine();
            if (!DateTime.TryParse(inputDate, out properInputDate)) { properInputDate = DateTime.Today; } else { ; }
            MenuManager.DrawWindow("calendar_input");
            
            string? calendarIdInput = Console.ReadLine()?.Trim()?.ToLower() ?? "gregorian";
            string calendarId = string.IsNullOrEmpty(calendarIdInput) ? "gregorian" : calendarIdInput;

            string converted = ConvertToCalendar(properInputDate, calendarId);

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

                case "1":
                    return CalendarConversion.ConvertTo_JC(date);

                case "2":
                    return CalendarConversion.ConvertTo_JOC(date);

                case "3":
                    return CalendarConversion.ConvertTo_NYC(date);

                case "4":
                    return CalendarConversion.ConvertTo_JUC(date);

                case "5":
                    return CalendarConversion.ConvertTo_MC(date);

                case "6":
                    return CalendarConversion.ConvertTo_OPC(date);

                case "7":
                    return CalendarConversion.ConvertTo_OMC(date);

                case "8":
                    return CalendarConversion.ConvertTo_RC(date);

                case "9":
                    return CalendarConversion.ConvertTo_GTC(date);

                case "a":
                    return CalendarConversion.ConvertToAllCalendars(date);

                default:
                    return "Unknown calendar ID.";
            }
        }
    }
}