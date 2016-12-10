using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Utils
{
    /// <summary>Provides better random methods</summary>
    public static class RandomEx
    {
        private static Random rand = new Random(); //Random object

        /// <summary>Generates a random number smaller the spessifed max but greater or equal to the spesifed min</summary>
        /// <param name="min">The minimu that the number can be</param>
        /// <param name="max">The max that the number is smaller then</param>
        /// <returns>A intager greater or equal to the min and smaller then the max</returns>
        public static int GenerateRandom(int min, int max)
        {
            return rand.Next(min, max);
        }

        /// <param name="min">The minimu that the number can be</param>
        /// <param name="max">The max that the number is smaller then</param>
        /// <returns>A intager greater or equal to the min and smaller then the max</returns>
        public static double GenerateRandom(float min, float max)
        {
            return min + (rand.NextDouble() * (max - min));
        }
    }
}