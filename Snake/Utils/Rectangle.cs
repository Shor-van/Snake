using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Utils
{
    /// <summary></summary>
    struct Rectangle
    {
        //Static members
        public static Rectangle Empty { get { return new Rectangle(0, 0, 0, 0); } }

        //Properties
        public int X, Y; //The top left coordinates of the rectangle on the screen
        public int Width, Height; //The width an d height of the rectangle 

        /// <summary>Creates a rectangle object</summary>
        /// <param name="x">The left location of the rectangle on the screen</param>
        /// <param name="y">The top location of the rectangle on the screen</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>Checks if the rectangle is intersecting with an other rectangle</summary>
        /// <param name="targetRectangle">The rectangle to check if this rectangle is intersecting with</param>
        /// <returns>True if the rectangle is intersecting with the spesified rectangle</returns>
        public bool Intersects(Rectangle targetRectangle)
        {
            if(X < 0)
                return true;
            return false;
        }
    }
}
