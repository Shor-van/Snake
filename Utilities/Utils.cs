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
    }
}