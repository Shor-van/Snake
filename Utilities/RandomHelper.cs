using System;

namespace Snake.Utilities
{
    /// <summary>Class that holds methods that help with generating random numbers</summary>
    internal static class RandomHelper
    {
        private static readonly Random random = new Random(); //random generator object with random seed

        /// <summary>Gets a random int</summary>
        /// <returns>A random in value</returns>
        internal static int RandomInt() => random.Next(); 

        /// <summary>Gets a random int between the given min and max value</summary>
        /// <param name="min">The minium possible value that can be returned</param>
        /// <param name="max">The maximum possibale value that can be returned</param>
        /// <returns>A random int between the given min and max</returns>
        internal static int RandomInt(int min, int max) => random.Next(min, max);

        /// <summary>Gets a random double between the given min and max value</summary>
        /// <param name="min">The minium possible value that can be returned</param>
        /// <param name="max">The maximum possibale value that can be returned</param>
        /// <returns>A random double between the given min and max</returns>
        internal static double RandomDouble(float min, float max) => min + (random.NextDouble() * (max - min));
    }
}