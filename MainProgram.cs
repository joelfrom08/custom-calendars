using System.Text;

namespace PetByte.CustomCalendars {
    static class MainProgram {
        static int consoleWidth = 0;
        static int consoleHeight = 0;

        public static DateTime properInputDate;

        static void Main(string[] args) {
            AppDomain.CurrentDomain.ProcessExit += ProcessEnd;
            Console.CancelKeyPress += ProcessEnd;
            Console.CursorVisible = false;
            Task.Run(() => CheckForResize());

            string inputDate = ReadRestrictedInput(10, c => char.IsDigit(c) || c == '-');
            if (!DateTime.TryParse(inputDate, out properInputDate)) { properInputDate = DateTime.Today; } else { ; }
            MenuManager.DrawWindow("calendar_input");

            string inputCalendar = ReadRestrictedInput(1, c => char.IsDigit(c) && c != '0' || c == 'a');
            string calendarId = inputCalendar.Length == 1 ? inputCalendar : "a";

            string converted = ConvertToCalendar(properInputDate, calendarId);

            Console.WriteLine(converted);
        }
        
        static void ProcessEnd(object? sender, EventArgs e) {
            Console.Clear();
            Console.CursorVisible = true;
            if (e is ConsoleCancelEventArgs) {
                Console.Write("\x1b[33mGoodbye.");
            } else {
                Console.WriteLine("\x1b[33mGoodbye.");
            }
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
        
        static string ReadRestrictedInput(int maxLength, Func<char, bool> isValidCharacter) {
            var input = new StringBuilder();

            while (true) {
                var key = Console.ReadKey(intercept: true);
                
                if (key.Key == ConsoleKey.Enter) {
                    Console.WriteLine();
                    break;
                } else if (key.Key == ConsoleKey.Backspace) {
                    if (input.Length > 0) {
                        input.Length--;
                        Console.CursorLeft -= 1;
                        Console.Write(" ");
                        Console.CursorLeft -= 1;
                    }
                } else if (input.Length < maxLength && isValidCharacter(key.KeyChar)) {
                    input.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
            
            return input.ToString();
        }
    }
}