# Custom Calendar Converter
## What is this?
In 2024, I created several custom calendar systems. Some are simple modifications of the Gregorian calendar, while others follow completely different structures.

This terminal tool allows you to convert any date from **0001-01-01** to **9999-12-31** to one of nine unique calendar systems.

## Usage
Download the executable for your operating system from the [releases page](https://github.com/joelfrom08/custom-calendars/releases/latest) and follow the on-screen instructions after launch.

<p align="center">
  <img src="https://assets.petbyte.dev/git_projects/custom-calendars/v1.0.0demo.gif" alt="GIF demonstrating basic usage in v1.0.0 (macOS Terminal 26.0.0)"><br>
  <em style="color: #666; font-style: italic;">GIF demonstrating basic usage in v1.0.0 (macOS Terminal 26.0.0)</em>
</p>

**Supported platforms:**
- `win-x64` (Windows 64-bit)
    - Tested on Windows 10 22H2 using Modern Terminal and Git Bash\* ***(SEE BELOW)***
- `osx-arm64` (macOS Apple Silicon)
    - Tested on M1 MacBook Pro (2020), macOS 26.0.0, using default terminal
- `osx-x64` (macOS Intel)
    - Tested on M1 MacBook Pro (2020), macOS 26.0.0, using default terminal
- `linux-x64` (Linux 64-bit)
    - Tested on Ubuntu 25.04 (Kernel 6.14.0-15-generic) using the default terminal
- *More may be coming in a future release*

> ⚠️🐛 **Known Issue — Windows Support**
> 
> The Windows build currently, as of *v1.0.1*, has major display issues in Windows 10 and 11 Command Prompt and PowerShell. Core functionality *does* work, it's just easily legible.
> 
> Using Git Bash or the modern Windows Terminal results in proper display, though crashes on startup.
> - **Status:** A fix will be shipped with *v1.0.2*, though it will REQUIRE [Modern Terminal](https://aka.ms/terminal). Support for Command Prompt and PowerShell will NOT be supported, though support MAY be added with *v2.0.0*.

**How to launch:**
- `osx-arm64`, `osx-x64`, `linux-x64`: Using your terminal of choice, navigate to the directory to which you downloaded the executable, and run `./CustomCalendars-[OS]`. Replace `[OS]` with `macos-arm`, `macos-x64`, or `linux` respectively.
- `win-x64`: Simply double-click the `.exe` file.

To quit, either cancel the execution using `⌃C`, or press enter after viewing the final result.
A nicer interface to quit will be implemented soon.

## Issues?
Please open an issue if you encounter any bugs! As this is a personal project, I can't guarantee specific response times.

## License
MIT License — see [LICENSE](LICENSE) file for details.

## Contributing
I appreciate the interest, but as this is a personal project, I'm not accepting pull requests at this time.

Feel free to fork this project for your own use!

## Roadmap
I have a few ideas I want to still implement, read below...
### v1.0.*x*
- Fix Windows-specific problems
- Small tweaks
### v1.1.*x*
- Convert *from* a custom calendar system back the Gregorian one
- Tweak date input
### v1.2.*x*
- Convert an entire year
- Tweak final result window & allow for easy quitting or new conversions without restarting the application
### v1.3.*x*
- Potentially add support for other platforms
- Information window(s)