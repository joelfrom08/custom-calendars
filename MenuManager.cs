using System.Numerics;

namespace PetByte.CustomCalendars {
    static class MenuManager {
        public static string titleString = "Custom Calendar Converter";
        public static string versionString = $"v{(ThisAssembly.IsPublicRelease ? ThisAssembly.AssemblyFileVersion.Remove(5, 2) : ThisAssembly.AssemblyInformationalVersion)}";
        public static string copyrightString = "(c) 2025 PetByte";
        public static string titleToVersionGradient = "";

        public static bool versionStringAtTop = true;
        public static bool versionStringVisible = true;
        public static bool titleStringVisible = true;
        public static bool copyrightStringVisible = true;

        static Vector2 currentWindowTL = Vector2.Zero;
        public static string currentWindow = "date_input";
        public static string temporaryInput = "";

        public static Dictionary<string, WindowInfo> windows = new() {
            {
                "date_input",
                new WindowInfo(
                    windowName: "Input date…",
                    topLeftOffset: new (2, 1),
                    finalPosition: new (5, 2),
                    windowSize: new (21, 7),
                    lines: new() {
                        "\x1b[1;3;38;2;160;160;160;48;2;192;192;192m   YYYY-MM-DD",
                        "\x1b[22;48;2;192;192;192m   \x1b[0m          ",
                        "\x1b[3;38;2;160;160;160;48;2;192;192;192m (empty = today)",
                        "",
                        " \x1b[23;1;38;2;0;192;0mENTER = CONFIRM\x1b[0m"
                    }
                )
            },
            {
                "calendar_input",
                new WindowInfo(
                    windowName: "Convert $DATE to…",
                    topLeftOffset: new (2, 1),
                    finalPosition: new (21, 1),
                    windowSize: new (30, 17),
                    lines: new() {
                        "\x1b[1;3;38;2;160;160;160;48;2;192;192;192mENTER CALENDAR ID: \x1b[0m ",
                        "\x1b[3;38;2;160;160;160;48;2;192;192;192m (empty = all)",
                        "",
                        "\x1b[38;2;0;0;0m1 — Der joel\'sche Kalender",
                        "2 — JoCalendar",
                        "3 — Nicer Years Calendar",
                        "4 — JulCal",
                        "5 — Millennium Calendar",
                        "6 — OpCal",
                        "7 — OmCal",
                        "8 — Retrollennium Calendar",
                        "9 — GoodTimes Calendar",
                        "a — (All Calendars)",
                        "",
                        " \x1b[1;38;2;0;192;0mENTER = CONFIRM\x1b[0m"
                    }
                )
            },
            {
                "finished_result",
                new WindowInfo(
                    windowName: "Conversion Result",
                    topLeftOffset: new (2, 1),
                    finalPosition: new (0, 0),
                    windowSize: new (67, 16),
                    lines: new() { }
                )
            }
        };

        public static void CalculateBoundaries() {
            if (versionString.Length + copyrightString.Length + 1 > Console.BufferWidth) { versionStringVisible = false; } else { versionStringVisible = true; }
            if (versionString.Length + titleString.Length + 7 > Console.BufferWidth) { versionStringAtTop = false; } else { versionStringAtTop = true; }

            if (titleString.Length > Console.BufferWidth) { titleStringVisible = false; } else { titleStringVisible = true; }

            if (copyrightString.Length + 2 > Console.BufferWidth) { copyrightStringVisible = false; } else { copyrightStringVisible = true; }
        }

        public static void CalculateTitleVersionGradient() {
            titleToVersionGradient = "";
            int steps = Console.BufferWidth - titleString.Length - versionString.Length;
            if (steps < 3) { return; }
            (int r, int g, int b) start = (255, 111, 0);
            (int r, int g, int b) end = (102, 102, 102);

            for (double i = 0; i < steps; i++) {
                double gradientPosition = i / (steps - 1);
                int r = (int)(start.r + (end.r - start.r) * gradientPosition);
                int g = (int)(start.g + (end.g - start.g) * gradientPosition);
                int b = (int)(start.b + (end.b - start.b) * gradientPosition);

                titleToVersionGradient += $"\x1b[48;2;{r};{g};{b}m \x1b[0m";
            }
        }

        public static void DrawMenuBackground() {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
        }

        public static void DrawTitle() {
            if (!titleStringVisible) { return; }

            Console.SetCursorPosition(0, 0);
            Console.Write($"\x1b[1;3;30;48;2;255;111;0m{titleString}");
            if (versionStringAtTop) {
                Console.SetCursorPosition(Console.BufferWidth - versionString.Length - titleToVersionGradient.Count(x => x == ' '), 0);
                Console.Write(titleToVersionGradient);
            } else {
                Console.Write("\x1b[22;38;2;255;111;0m" + new string('█', Console.BufferWidth - titleString.Length));
            }

            Console.ResetColor();
            Console.Write("\x1b[0m");
        }

        public static void DrawVersion() {
            if (!versionStringVisible) { return; }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            int x = Console.BufferWidth - versionString.Length;
            int y = versionStringAtTop ? 0 : Console.BufferHeight;
            Console.SetCursorPosition(x, y);
            Console.Write($"\x1b[1m{versionString}\x1b[22m");

            Console.ResetColor();
        }

        public static void DrawCopyright() {
            if (!copyrightStringVisible) { return; }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write($"\x1b[1m{copyrightString}\x1b[22m");
            if (!versionStringAtTop || !versionStringVisible) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(new string('█', Console.BufferWidth - copyrightString.Length - (versionStringVisible ? versionString.Length : 0)));
            }

            Console.ResetColor();
        }

        public static void DrawWindow(string windowID) {
            currentWindow = windowID;
            int windowWidth = (int)windows[windowID].windowSize.X;
            int windowHeight = (int)windows[windowID].windowSize.Y;
            string windowTitle = windows[windowID].windowName;
            windowTitle = windowTitle.Replace("$DATE", $"{MainProgram.properInputDate:yyyy-MM-dd}");
            
            for (int i = 0; i < windowHeight; i++) {
                Console.SetCursorPosition((Console.BufferWidth - windowWidth) / 2, ((Console.BufferHeight - windowHeight) / 2) + i);
                if (i == 0) {
                    Console.Write("\x1b[38;2;160;160;160;48;2;192;192;192m╔");
                    Console.Write(new string('═', (windowWidth - windowTitle.Length) / 2 - 2));
                    Console.Write("\x1b[38;2;192;0;0m " + windowTitle + " ");
                    Console.Write("\x1b[38;2;160;160;160m" + new string('═', (int)Math.Ceiling((double)(windowWidth - windowTitle.Length) / 2) - 2));
                    Console.Write("╗");
                    Console.Write("\x1b[38;2;0;0;192;48;2;0;0;255m▄▄");
                } else if (i > 0 && i < windowHeight - 1) {
                    Console.Write("\x1b[38;2;160;160;160;48;2;192;192;192m║\x1b[38;2;192;192;192m" + new string('█', windowWidth - 2) + "\x1b[38;2;160;160;160m║\x1b[38;2;0;0;192;48;2;0;0;192m██");
                } else {
                    Console.Write("\x1b[38;2;160;160;160;48;2;192;192;192m╚");
                    Console.Write("" + new string('═', windowWidth - 2));
                    Console.Write("╝");
                    Console.Write("\x1b[38;2;0;0;192;48;2;0;0;192m██");
                    Console.SetCursorPosition(((Console.BufferWidth - windowWidth) / 2) + 1, ((Console.BufferHeight - windowHeight) / 2) + i + 1);
                    Console.Write(new string('█', windowWidth + 1) + "\x1b[0m");
                }
            }

            currentWindowTL = new Vector2((Console.BufferWidth - windowWidth) / 2, (Console.BufferHeight - windowHeight) / 2);
            if (windowID == "finished_result") { DrawResult(); } else { DrawWindowContents(windowID); }
        }

        static void DrawWindowContents(string windowID) {
            for (int i = 0; i < windows[windowID].lines.Count; i++) {
                Console.SetCursorPosition((int)(currentWindowTL.X + windows[windowID].topLeftOffset.X), (int)(currentWindowTL.Y + windows[windowID].topLeftOffset.Y + i));
                Console.Write(windows[windowID].lines[i]);
            }
            Console.SetCursorPosition((int)(currentWindowTL.X + windows[windowID].finalPosition.X), (int)(currentWindowTL.Y + windows[windowID].finalPosition.Y));
            Console.Write(temporaryInput);
        }
        
        public static void DrawWindowTooSmall() {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write("╔" + new string('═', Console.BufferWidth - 2) + "╗");
            for (int i = 1; i < Console.BufferHeight - 1; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
                Console.SetCursorPosition(Console.BufferWidth - 1, i);
                Console.Write("║");
            }
            Console.Write("╚" + new string('═', Console.BufferWidth - 2) + "╝");

            List<string> warningText = new List<string> { "Window too small", $"({Console.BufferWidth}x{Console.BufferHeight}). Set to", "80x24 or larger." };
            foreach (string warning in warningText) {
                Console.SetCursorPosition((Console.BufferWidth - warning.Length) / 2, (int)Math.Ceiling((double)(Console.BufferHeight / 2)) - 1 + warningText.IndexOf(warning));
                Console.Write("\x1b[1;38;2;255;0;0m" + warning);
            }
            Console.Write("\x1b[0m");
            Console.ResetColor();
        }
        
        static void DrawResult() {
            Console.SetCursorPosition((int)currentWindowTL.X + 2, (int)currentWindowTL.Y + 1);
            Console.Write("\x1b[30;48;2;192;192;192m");
            Console.Write($"The Gregorian date \x1b[1m{MainProgram.properInputDate:yyyy-MM-dd}\x1b[22m converts to…");
            if (MainProgram.calendarID == 'a') {
                for (int i = 0; i < CalendarConversion.convertToCalendarFunctions.Count; i++) {
                    string calendarName = CalendarConversion.calendars.ElementAt(i).Value.calendarName;
                    Console.SetCursorPosition((int)currentWindowTL.X + 2, (int)currentWindowTL.Y + 3 + i);
                    Console.Write($"{new string(' ', 22 - calendarName.Length)}{calendarName}: " + CalendarConversion.convertToCalendarFunctions[i]);
                }
            } else {
                Console.SetCursorPosition((int)currentWindowTL.X + 2, (int)currentWindowTL.Y + 3);
                string calendarName = CalendarConversion.calendars.ElementAt(MainProgram.calendarID - '1').Value.calendarName;
                Console.Write($"{calendarName}: " + CalendarConversion.convertToCalendarFunctions[MainProgram.calendarID - '1'] + "\x1b[0m");
            }

            Console.SetCursorPosition((int)currentWindowTL.X + 2, (int)(currentWindowTL.Y + windows[currentWindow].windowSize.Y - 2));
            Console.Write("\x1b[1;3;32;48;2;192;192;192mPress any key to exit.");
            Console.Write("\x1b[0m");
        }

        public static void DrawInvalidInput() {
            Console.SetCursorPosition((Console.BufferWidth - 29) / 2, 1);
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Invalid Input. Leave empty or");
            Console.SetCursorPosition((Console.BufferWidth - 32) / 2, 2);
            Console.Write("write in full YYYY-MM-DD format.");
            Thread.Sleep(1500);
            temporaryInput = "";
            ResetScreen();
        }

        public static void ResetScreen(){
            Console.ResetColor();
            Console.Clear();
            DrawMenuBackground();
            CalculateBoundaries();
            CalculateTitleVersionGradient();
            DrawTitle();
            DrawVersion();
            DrawCopyright();
            DrawWindow(currentWindow);
        }
    }
}