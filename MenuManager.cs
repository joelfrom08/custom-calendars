class MenuManager {
    public static string titleString = "Custom Calendar Converter";
    public static string versionString = "v1.0.0-beta.23";
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

        if (copyrightString.Length + 10 > Console.WindowWidth) { copyrightStringVisible = false; } else { copyrightStringVisible = true; }
    }

    public static void CalculateTitleVersionGradient() {
        titleToVersionGradient = "";
        int steps = Console.WindowWidth - titleString.Length - versionString.Length;
        if (steps < 3) { return; }
        (int r, int g, int b) start = (255, 111, 0);
        (int r, int g, int b) end = (128, 128, 128);

        for (double i = 0; i < steps; i++) {
            double gradientPosition = i / (steps - 1);
            int r = (int)(start.r + (end.r - start.r) * gradientPosition);
            int g = (int)(start.g + (end.g - start.g) * gradientPosition);
            int b = (int)(start.b + (end.b - start.b) * gradientPosition);

            titleToVersionGradient += $"\x1b[38;2;{r};{g};{b};48;2;{r};{g};{b}m█";
        }
    }

    public static void DrawMenuBackground() {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();
    }

    public static void DrawTitle() {
        if (!titleStringVisible) { return; }

        Console.SetCursorPosition(0, 0);
        Console.Write($"\x1b[1;3;38;2;0;0;0;48;2;255;111;0m{titleString}\x1b[0m");
        if (versionStringAtTop) {
            Console.SetCursorPosition(Console.WindowWidth - versionString.Length - titleToVersionGradient.Count(x => x == '█'), 0);
            Console.Write(titleToVersionGradient);
        } else {
            Console.Write("\x1b[38;2;255;111;0;48;2;255;111;0m" + new string('█', Console.WindowWidth-titleString.Length));
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
        Console.Write($"\x1b[1m{versionString}");

        Console.ResetColor();
    }
    
    public static void DrawCopyright() {
        if (!copyrightStringVisible) { return; }
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkGray;

        Console.SetCursorPosition(0, Console.WindowHeight);
        Console.Write($"\x1b[1m{copyrightString}");
        if (!versionStringAtTop || !versionStringVisible) {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('█', Console.WindowWidth - copyrightString.Length - (versionStringVisible ? versionString.Length : 0)));
        }

        Console.ResetColor();
    }
}