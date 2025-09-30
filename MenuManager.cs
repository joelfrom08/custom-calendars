namespace PetByte.CustomCalendars {
    class MenuManager {
        public static string titleString = "Custom Calendar Converter";
        public static string versionString = $"{(ThisAssembly.GitCommitId).Substring(0, 10)}";
        public static string copyrightString = "(c) 2025 PetByte";
        public static string titleToVersionGradient = "";

        public static bool versionStringAtTop = true;
        public static bool versionStringVisible = true;
        public static bool titleStringVisible = true;
        public static bool copyrightStringVisible = true;

        public static void CalculateBoundaries() {
            if (versionString.Length + copyrightString.Length + 1 > Console.WindowWidth) { versionStringVisible = false; } else { versionStringVisible = true; }
            if (versionString.Length + titleString.Length + 7 > Console.WindowWidth) { versionStringAtTop = false; } else { versionStringAtTop = true; }

            if (titleString.Length > Console.WindowWidth) { titleStringVisible = false; } else { titleStringVisible = true; }

            if (copyrightString.Length + 2 > Console.WindowWidth) { copyrightStringVisible = false; } else { copyrightStringVisible = true; }
        }

        public static void CalculateTitleVersionGradient() {
            titleToVersionGradient = "";
            int steps = Console.WindowWidth - titleString.Length - versionString.Length;
            if (steps < 3) { return; }
            (int r, int g, int b) start = (255, 111, 0);
            (int r, int g, int b) end = (102, 102, 102);

            for (double i = 0; i < steps; i++) {
                double gradientPosition = i / (steps - 1);
                int r = (int)(start.r + (end.r - start.r) * gradientPosition);
                int g = (int)(start.g + (end.g - start.g) * gradientPosition);
                int b = (int)(start.b + (end.b - start.b) * gradientPosition);

                titleToVersionGradient += $"\x1b[38;2;{r};{g};{b};48;2;{r};{g};{b}m█\x1b[0m";
            }
        }

        public static void DrawMenuBackground() {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
        }

        public static void DrawTitle() {
            if (!titleStringVisible) { return; }

            Console.SetCursorPosition(0, 0);
            Console.Write($"\x1b[1;3;38;2;0;0;0;48;2;255;111;0m{titleString}");
            if (versionStringAtTop) {
                Console.SetCursorPosition(Console.WindowWidth - versionString.Length - titleToVersionGradient.Count(x => x == '█'), 0);
                Console.Write(titleToVersionGradient);
            } else {
                Console.Write("\x1b[22;3;38;2;255;111;0m" + new string('█', Console.WindowWidth - titleString.Length));
            }

            Console.ResetColor();
            Console.Write("\x1b[0m");
        }

        public static void DrawVersion() {
            if (!versionStringVisible) { return; }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            int x = Console.WindowWidth - versionString.Length;
            int y = versionStringAtTop ? 0 : Console.WindowHeight;
            Console.SetCursorPosition(x, y);
            Console.Write($"\x1b[1m{versionString}\x1b[22m");

            Console.ResetColor();
        }

        public static void DrawCopyright() {
            if (!copyrightStringVisible) { return; }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(0, Console.WindowHeight);
            Console.Write($"\x1b[1m{copyrightString}\x1b[22m");
            if (!versionStringAtTop || !versionStringVisible) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(new string('█', Console.WindowWidth - copyrightString.Length - (versionStringVisible ? versionString.Length : 0)));
            }

            Console.ResetColor();
        }

        public static void DrawWindow(int windowWidth, int windowHeight, string windowTitle) {
            for (int i = 0; i < windowHeight; i++) {
                Console.SetCursorPosition((Console.WindowWidth - windowWidth) / 2, ((Console.WindowHeight - windowHeight) / 2) + i);
                if (i == 0) {
                    Console.Write("\x1b[38;2;160;160;160;48;2;192;192;192m╔");
                    Console.Write(new string('═', (windowWidth - windowTitle.Length) / 2 - 2));
                    Console.Write("\x1b[38;2;192;0;0m " + windowTitle + " ");
                    Console.Write("\x1b[38;2;160;160;160m" + new string('═', (int)Math.Ceiling((double)(windowWidth - windowTitle.Length) / 2) - 2));
                    Console.Write("╗");
                    Console.Write("\x1b[38;2;0;0;192;48;2;0;0;255m▄▄");
                } else if (i > 0 && i < windowHeight - 1) {
                    Console.Write("\x1b[38;2;160;160;160;48;2;192;192;192m║\x1b[38;2;192;192;192m" + new string('█', windowWidth - 2) + "\x1b[38;2;160;160;160;48;2;192;192;192m║\x1b[38;2;0;0;192;48;2;0;0;192m██");
                } else {
                    Console.Write("\x1b[38;2;160;160;160;48;2;192;192;192m╚");
                    Console.Write("" + new string('═', windowWidth - 2));
                    Console.Write("╝");
                    Console.Write("\x1b[38;2;0;0;192;48;2;0;0;192m██");
                    Console.SetCursorPosition(((Console.WindowWidth - windowWidth) / 2) + 1, ((Console.WindowHeight - windowHeight) / 2) + i + 1);
                    Console.Write(new string('█', windowWidth + 1) + "\x1b[0m");
                }
            }
        }
    }
}