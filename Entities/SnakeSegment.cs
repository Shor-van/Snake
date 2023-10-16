using System;

namespace Snake.Entities
{
    /// <summary>The differant type of snake segments</summary>
    internal enum SegmentType { Body = 0, Head = 1, Tail = 2, }
    
    /// <summary>rRepresents a snake segment</summary>    
    internal sealed class SnakeSegment
    {
        private int x; //the X loctaion of the segment
        private int y; //the Y location of the segment
        private SegmentType type; //the type of snake segment this is

        /// <summary>Get the X position of the segment</summary>
        internal int X => x;

        /// <summary>Get the Y position of the segment</summary>
        internal int Y => y;

        internal SnakeSegment(int x, int y, SegmentType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        /// <summary>Checks if the given X/Y values intersect with the segment</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the given location intersects with theS segments</returns>
        internal bool Intersects(int x, int y) => this.x == x && this.y == y;

        /// <summary>Set the position of the segment to an other segment</summary>
        /// <param name="other">The other segment of the position to set to</param>
        internal void SetPosition(SnakeSegment other) => SetPosition(other.x, other.y);

        /// <summary>Set the position of the segment</summary>
        /// <param name="x">The new X location to set</param>
        /// <param name="y">The new Y location to set</param>
        internal void SetPosition(int x, int y) 
        { 
            //TODO: add valiation, x/y cannot be less then 0 or greather then buffer

            this.x = x; 
            this.y = y; 
        }

        /// <summary>Draws the segment to the console</summary>
        internal void Draw()
        {
            char sgementChar = Snake.bodySegment;
            if (type == SegmentType.Head) sgementChar = Snake.headSegment;
            else if (type == SegmentType.Tail) sgementChar = Snake.tailSegment;

            Console.SetCursorPosition(x, y);
            Console.Write(sgementChar);
        }
    }
}