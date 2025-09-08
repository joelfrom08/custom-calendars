using System;

class Program {
    static void Main(string[] args) {
        Console.WriteLine("Enter a date (YYYY-MM-DD):");
        string inputDate = Console.ReadLine();

        // Parse input into a DateTime
        if (!DateTime.TryParse(inputDate, out DateTime gregorianDate)) {
            Console.WriteLine("Invalid date format. Use YYYY-MM-DD.");
            return;
        }

        Console.WriteLine("Enter calendar ID (e.g., custom):");
        string calendarId = Console.ReadLine()?.Trim().ToLower();

        string converted = ConvertToCalendar(gregorianDate, calendarId);

        Console.WriteLine($"Gregorian {gregorianDate:yyyy-MM-dd} in {calendarId} calendar: {converted}");
    }

    static string ConvertToCalendar(DateTime date, string calendarId) {
        switch (calendarId) {
            case "test_cal":
                return Convert_TestCal(date);

            default:
                return "Unknown calendar ID.";
        }
    }

    static string Convert_TestCal(DateTime date) {
        int year = date.Year + 100;
        int month = date.Month;
        int day = date.Day;

        return $"{year}-{month:D2}-{day:D2}";
    }
}
