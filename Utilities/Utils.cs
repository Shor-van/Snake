using System;

namespace Snake.Utilities
{
    /// <summary>Static class that holds 'Utility' methods</summary>
    internal static class Utils
    {
        /// <summary>Checks if the given X/Y location is within the <see cref="Console"/> window boundery</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the location is within the boundery, false if not</returns>
        internal static bool IsWithinWindowBoundery(int x, int y) => x >= 0 && x < Console.BufferWidth && y >= 0 && y < Console.BufferHeight;

        /// <summary>Checks if the given 'rectangle' is within the <see cref="Console"/> window boundery</summary>
        /// <param name="x">The left most point of the rectangle</param>
        /// <param name="x">The top most point of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>True if the 'rectangle' is within the boundery, false if not</returns>
        internal static bool IsWithinWindowBoundery(int x, int y, int width, int height) => x >= 0 && x < Console.BufferWidth && y >= 0 && y < Console.BufferHeight && x + width < Console.BufferWidth && y + height < Console.BufferHeight;
    }
}