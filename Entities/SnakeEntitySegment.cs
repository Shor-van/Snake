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
        private readonly SegmentType type; //the type of snake segment this is

        /// <summary>Get the X position of the segment</summary>
        internal int X => x;

        /// <summary>Get the Y position of the segment</summary>
        internal int Y => y;

        internal SnakeEntitySegment(int x, int y, SegmentType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        /// <summary>Draws the segment to the console</summary>
        internal void Draw()
        {
            char sgementChar = SnakeEntity.bodySegment;
            if (type == SegmentType.Head) sgementChar = SnakeEntity.headSegment;
            else if (type == SegmentType.Tail) sgementChar = SnakeEntity.tailSegment;

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
            if(Utils.IsWithinWindowBoundery(X, y) == false) //Check that X/Y is within the window boundery //TODO: change this to be 'play area'
                throw new ArgumentOutOfRangeException(nameof(x) + "/" + nameof(y) + " is not widthin the console window boundery.", nameof(x) + "/" + nameof(y));

            this.x = x; 
            this.y = y; 
        }
    }
}