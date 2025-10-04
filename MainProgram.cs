using System.Text;

namespace PetByte.CustomCalendars {
    static class MainProgram {
        static int consoleWidth = 0;
        static int consoleHeight = 0;

        public static DateTime properInputDate;
        public static char calendarID;

        static void Main(string[] args) {
            MenuManager.ResetScreen();
            AppDomain.CurrentDomain.ProcessExit += ProcessEnd;
            Console.CancelKeyPress += ProcessEnd;
            Console.CursorVisible = false;
            Task.Run(() => CheckForResize());

            string inputDate = ReadRestrictedInput(10, c => char.IsDigit(c) || c == '-');
            if (!DateTime.TryParse(inputDate, out properInputDate)) { properInputDate = DateTime.Today; } else {; }
            MenuManager.DrawWindow("calendar_input");
            MenuManager.temporaryInput = "";
            MenuManager.ResetScreen();

            string inputCalendar = ReadRestrictedInput(1, c => char.IsDigit(c) && c != '0' || c == 'a');
            calendarID = inputCalendar.Length == 1 ? inputCalendar.First() : 'a';

            MenuManager.DrawWindow("finished_result");
            MenuManager.temporaryInput = "";
            MenuManager.ResetScreen();
            Console.ReadLine();
        }
        
        static void ProcessEnd(object? sender, EventArgs e) {
            Console.ResetColor();
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
                    if (Console.WindowHeight < 24 || Console.WindowWidth < 80) {
                        MenuManager.DrawWindowTooSmall();
                    } else {
                        MenuManager.ResetScreen();
                    }

                    consoleWidth = Console.WindowWidth;
                    consoleHeight = Console.WindowHeight;
                }
                Thread.Sleep(1);
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
                        MenuManager.temporaryInput = MenuManager.temporaryInput.Remove(MenuManager.temporaryInput.Length - 1, 1);
                        Console.CursorLeft -= 1;
                        Console.Write(" ");
                        Console.CursorLeft -= 1;
                    }
                } else if (input.Length < maxLength && isValidCharacter(key.KeyChar)) {
                    input.Append(key.KeyChar);
                    MenuManager.temporaryInput += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            
            return input.ToString();
        }
    }
}