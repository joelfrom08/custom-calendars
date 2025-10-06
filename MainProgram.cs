using System.Runtime.InteropServices;
using System.Text;

namespace PetByte.CustomCalendars {
    static class MainProgram {
        static int consoleWidth = 0;
        static int consoleHeight = 0;

        public static DateTime properInputDate;
        public static char calendarID;
        public static bool windowTooSmall = false;

        static void Main(string[] args) {
            CheckIfModernTerminal();
            MenuManager.ResetScreen();
            AppDomain.CurrentDomain.ProcessExit += ProcessEnd;
            Console.CancelKeyPress += ProcessEnd;
            Console.CursorVisible = false;
            Task.Run(() => CheckForResize());

            string conversionDirection = ReadRestrictedInput(1, c => char.IsDigit(c) && c == '1' || c == '2');
            if (string.IsNullOrEmpty(conversionDirection)) { conversionDirection = "1"; }
            MenuManager.DrawWindow("date_input_greg");
            MenuManager.temporaryInput = "";
            MenuManager.ResetScreen();

            InputDate();
            MenuManager.DrawWindow("calendar_input");
            MenuManager.temporaryInput = "";
            MenuManager.ResetScreen();

            string inputCalendar = ReadRestrictedInput(1, c => char.IsDigit(c) && c != '0' || c == 'a');
            calendarID = inputCalendar.Length == 1 ? inputCalendar.First() : 'a';

            MenuManager.DrawWindow("finished_result");
            MenuManager.temporaryInput = "";
            MenuManager.ResetScreen();

            Console.ReadKey(true);
            Environment.Exit(0);
        }
        
        static void CheckIfModernTerminal() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
            string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TERM")) && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WT_SESSION"))) {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("This program requires a modern terminal such as, but not limited to:");
                Console.WriteLine("- Windows Terminal");
                Console.WriteLine("   - Download here: https://aka.ms/terminal");
                Console.WriteLine("- Git Bash");
                Console.WriteLine("   - Download here: https://git-scm.com/downloads/win");
                Console.WriteLine("Other Terminals may be supported but have not been tested.");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.CursorVisible = false;
                Console.ReadKey();
                Console.CursorVisible = true;
                Console.ResetColor();
                Environment.Exit(1);
            }
        }
        
        static void ProcessEnd(object? sender, EventArgs e) {
            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = true;
            if (e is ConsoleCancelEventArgs) {
                Console.Write("\x1b[33mGoodbye.\x1b[0m");
            } else {
                Console.WriteLine("\x1b[33mGoodbye.\x1b[0m");
            }
        }

        static void CheckForResize() {
            while (true) {
                if (Console.BufferWidth != consoleWidth || Console.BufferHeight != consoleHeight) {
                    if (Console.BufferHeight < 24 || Console.BufferWidth < 80) {
                        MenuManager.DrawWindowTooSmall();
                        windowTooSmall = true;
                    } else {
                        MenuManager.ResetScreen();
                        windowTooSmall = false;
                    }

                    consoleWidth = Console.BufferWidth;
                    consoleHeight = Console.BufferHeight;
                }
                Thread.Sleep(25);
            }
        }
        
        static string ReadRestrictedInput(int maxLength, Func<char, bool> isValidCharacter) {
            var input = new StringBuilder();

            while (true) {
                var key = Console.ReadKey(intercept: true);
                
                if (windowTooSmall) { continue; }
                
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

        static void InputDate() {
            string inputDate = ReadRestrictedInput(10, c => char.IsDigit(c) || c == '-');
            if (!DateTime.TryParse(inputDate, out properInputDate) && inputDate == "") {
                properInputDate = DateTime.Today;
            } else if (!DateTime.TryParse(inputDate, out properInputDate) && inputDate != "") {
                MenuManager.DrawInvalidInput();
                InputDate();
            } else { }
        }
    }
}