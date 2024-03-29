using System;

using Snake.Utilities;

namespace Snake.Entities
{
    /// <summary>Represents a piece of food that a snake can eat to 'grow'</summary>
    internal sealed class FoodEntity
    {
        internal const char foodChar = '$'; //the char used to draw the food

        private int x, y; //the X and Y position of the food

        internal FoodEntity(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        internal void Update()
        {

        }

        internal void Draw(DrawBuffer drawBuffer)
        {
            if(Utilities.Utils.IsWithinConsoleViewport(x, y) == false) return;

            drawBuffer.SetChar(x, y, foodChar);
        }

        /// <summary>Checks if the given X/Y values intersect with the food</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the given location intersects with the food, false if not</returns>
        internal bool Intersects(int x, int y) => this.x == x && this.y == y;

        /// <summary>Checks if the food lays within the specified 'rectangle'</summary>
        /// <param name="left">The left most point of the rectangle</param>
        /// <param name="top">The top most point of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>True if the given 'rectangle' intersects with the food</returns>
        internal bool Intersects(int left, int top, int width, int height) => x >= left && x < left + width && y >= top && y < top + height;
    }
}