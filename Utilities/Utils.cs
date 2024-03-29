using System;

namespace Snake.Utilities
{
    /// <summary>Static class that holds 'Utility' methods</summary>
    internal static class Utils
    {
        /// <summary>Checks if the given X/Y location is within the <see cref="Console"/> buffer</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the location is within the console buffer, false if not</returns>
        internal static bool IsWithinConsoleBuffer(int x, int y) => x >= 0 && x < Console.BufferWidth && y >= 0 && y < Console.BufferHeight;

        /// <summary>Checks if the given 'rectangle' is within the <see cref="Console"/> buffer</summary>
        /// <param name="left">The left most point of the rectangle</param>
        /// <param name="top">The top most point of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>True if the 'rectangle' is within the boundery, false if not</returns>
        internal static bool IsWithinConsoleBuffer(int left, int top, int width, int height) => left >= 0 && left < Console.BufferWidth && top >= 0 && top < Console.BufferHeight && left + width < Console.BufferWidth && top + height < Console.BufferHeight;

        /// <summary>Checks if the given X/Y location is within the <see cref="Console"/> viewport</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the location is within the console viewport, false if not</returns>
        internal static bool IsWithinConsoleViewport(int x, int y) => x >= Console.WindowLeft && x < Console.WindowLeft + Console.WindowWidth && y >= Console.WindowTop && y < Console.WindowTop + Console.WindowHeight;
    }
}