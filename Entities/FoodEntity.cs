using System;

namespace Snake.Entities
{
    /// <summary>Represents a piece of food that a snake can eat to 'grow'</summary>
    internal sealed class FoodEntity
    {
        internal const char foodChar = '$'; //the char used to draw the food

        private int x; //the X loctaion of the food
        private int y; //the Y location of the food

        internal FoodEntity(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        internal void Update()
        {

        }

        internal void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(foodChar);
        }

        /// <summary>Checks if the given X/Y values intersect with the food</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the given location intersects with the food, false if not</returns>
        internal bool Intersects(int x, int y) => this.x == x && this.y == y;
    }
}