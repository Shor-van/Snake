using System;

using Snake.Utilities;

namespace Snake.Entities
{
    /// <summary>The differant type of snake segments</summary>
    internal enum SegmentType { Body = 0, Head = 1, Tail = 2, }
    
    /// <summary>rRepresents a snake segment</summary>    
    internal sealed class SnakeEntitySegment
    {
        private int x; //the X loctaion of the segment
        private int y; //the Y location of the segment
        private int lastX; //the last X position of the segment
        private int lastY; //the last Y position of the segment
        private readonly SegmentType type; //the type of snake segment this is
        private readonly SnakeEntity snake; //the snake this segment belongs to

        /// <summary>Get the X position of the segment</summary>
        internal int X => x;

        /// <summary>Get the Y position of the segment</summary>
        internal int Y => y;

        /// <summary></summary>
        internal int LastX => lastX;

        /// <summary></summary>
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
            if(type == SegmentType.Tail || snake.FullRedraw == true && (x != lastX || y != lastY)) { 
                Console.SetCursorPosition(lastX, lastY);
                Console.Write(' ');
            }

            //reset last position and draw segment
            lastX = x; lastY = y;
            Console.SetCursorPosition(x, y);
            Console.Write(sgementChar);
        }

        /// <summary>Checks if the given X/Y values intersect with the segment</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the given location intersects with theS segments</returns>
        internal bool Intersects(int x, int y) => this.x == x && this.y == y;

        /// <summary>Set the position of the segment to an other segment</summary>
        /// <param name="other">The other segment of the position to set to</param>
        internal void SetPosition(SnakeEntitySegment other) => SetPosition(other.x, other.y);

        /// <summary>Set the position of the segment</summary>
        /// <param name="x">The new X location to set</param>
        /// <param name="y">The new Y location to set</param>
        internal void SetPosition(int x, int y) 
        { 
            if(Utils.IsWithinWindowBoundery(x, y) == false) //Check that X/Y is within the window boundery //TODO: change this to be 'play area'
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