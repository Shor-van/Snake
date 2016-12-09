using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Utils
{
    struct Vector2
    {
        //Static memebers
        public static Vector2 Zero = new Vector2(0, 0);

        //Properties
        public int X { get; set; } //The x component of the vector 
        public int Y { get; set; } //The y component of the vector

        /// <summary>Creates a new vector object with 2 components X and Y</summary>
        /// <param name="x">The x component of the vector</param>
        /// <param name="y">The y component of the vector</param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Calculates the distance from a spesified vector2</summary>
        /// <param name="targetVector">THe vector to calcuate the distance from</param>
        /// <returns>The distance from the spesified vector</returns>
        public double CalculateDistanceFrom(Vector2 targetVector)
        {
            double bas = Math.Pow(X - targetVector.X, 2) + Math.Pow(Y - targetVector.Y, 2);
            return Math.Sqrt(bas);
        }
    }
}
