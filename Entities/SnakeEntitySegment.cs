using System;

using Snake.Utilities;

namespace Snake.Entities
{
    /// <summary>The differant type of snake segments</summary>
    internal enum SegmentType { Body = 0, Head = 1, Tail = 2, }
    
    /// <summary>rRepresents a snake segment</summary>    
    internal sealed class SnakeEntitySegment
    {
        private int x, y; //the X and Y position of the segment
        private int lastX, lastY; //the last X and Y position of the segment
        private readonly SegmentType type; //the type of snake segment this is
        private readonly SnakeEntity snake; //the snake this segment belongs to

        /// <summary>Get the X position of the segment</summary>
        internal int X => x;

        /// <summary>Get the Y position of the segment</summary>
        internal int Y => y;

        /// <summary>Get the Last X position of the segment</summary>
        internal int LastX => lastX;

        /// <summary>Get the Last Y position of the segment</summary>
        internal int LastY => lastY;

        internal SnakeEntitySegment(int x, int y, SegmentType type, SnakeEntity snake)
        {
            lastX = this.x = x;
            lastY = this.y = y;
            this.type = type;
            this.snake = snake;
        }

        /// <summary>Draws the segment to the console</summary>
        internal void Draw()
        {
            //get the char used to draw this segment
            char sgementChar = SnakeEntity.bodySegment;
            if (type == SegmentType.Head) sgementChar = SnakeEntity.headSegment;
            else if (type == SegmentType.Tail) sgementChar = SnakeEntity.tailSegment;

            //clear the segment from the screen if we need
            if((type == SegmentType.Tail || snake.FullRedraw == true) && (x != lastX || y != lastY) && Utils.IsWithinConsoleViewport(lastX, lastY) == true) { 
                Console.SetCursorPosition(lastX, lastY);
                Console.Write(' ');
            }

            //reset last position and draw segment
            lastX = x; lastY = y;

            if(Utils.IsWithinConsoleViewport(x, y) == false) return;

            Console.SetCursorPosition(x, y);
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = snake.PrimaryColor;
            Console.Write(sgementChar);
            Console.ForegroundColor = current;
        }

        /// <summary>Checks if the given X/Y values intersect with the segment</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the given location intersects with the segment</returns>
        internal bool Intersects(int x, int y) => this.x == x && this.y == y;

        /// <summary>Checks if the segment lays within the specified 'rectangle'</summary>
        /// <param name="left">The left most point of the rectangle</param>
        /// <param name="top">The top most point of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>True if the given 'rectangle' intersects with the segment</returns>
        internal bool Intersects(int left, int top, int width, int height) => x >= left && x < left + width && y >= top && y < top + height;

        /// <summary>Set the position of the segment to an other segment</summary>
        /// <param name="other">The other segment of the position to set to</param>
        internal void SetPosition(SnakeEntitySegment other) => SetPosition(other.x, other.y);

        /// <summary>Set the position of the segment</summary>
        /// <param name="position">A <see cref="ValueTuple"/> containing the x and y value to set</param>
        internal void SetPosition((int x, int y) position) => SetPosition(position.x, position.y);

        /// <summary>Set the position of the segment</summary>
        /// <param name="x">The new X location to set</param>
        /// <param name="y">The new Y location to set</param>
        internal void SetPosition(int x, int y) 
        { 
            if(Utils.IsWithinConsoleBuffer(x, y) == false) //Check that X/Y is within the window boundery //TODO: change this to be 'play area'
                throw new ArgumentOutOfRangeException(nameof(x) + "/" + nameof(y) + " is not widthin the console window boundery.", nameof(x) + "/" + nameof(y));

            lastX = this.x; 
            lastY = this.y;

            this.x = x; 
            this.y = y; 
        }

        /// <summary>Reset the position of the segment to where it was</summary>
        internal void ResetPosition() { x = lastX; y = lastY; }
    }
}