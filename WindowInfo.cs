using System.Numerics;

namespace PetByte.CustomCalendars {
    class WindowInfo {
        public string windowName { get; set; }
        public Vector2 topLeftOffset { get; set; }
        public Vector2 finalPosition { get; set; }
        public Vector2 windowSize { get; set; }
        public List<string> lines { get; set; }

        public WindowInfo(string windowName, Vector2 topLeftOffset, Vector2 finalPosition, Vector2 windowSize, List<string> lines) {
            this.windowName = windowName;
            this.topLeftOffset = topLeftOffset;
            this.finalPosition = finalPosition;
            this.windowSize = windowSize;
            this.lines = lines;
        }
    }
}