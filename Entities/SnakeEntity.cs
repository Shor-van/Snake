using System;
using System.Collections.Generic;

using Snake.Utilities;

namespace Snake.Entities
{
    /// <summary>A list of move directions</summary>
    internal enum Direction { Up = 0, Down = 1, Left = 2, Right = 3 }

    /// <summary>Represents a snake entity</summary>
    internal sealed class SnakeEntity
    {
        internal const int minimumSnakeSize = 1; //then minimum a snake can be
        internal const char headSegment = 'H'; //the char used for the snake head segment
        internal const char bodySegment = 'B'; //the char used for the snake boday segment █
        internal const char tailSegment = 'T'; //the char used for the snake tail segment  ▲►▼◄

        private readonly List<SnakeEntitySegment> segments; //The snake's segments
        private readonly ConsoleColor primaryColor; //The snakes primary color
        private bool fullRedraw; //wether the snake should be completely redrawn
        private Direction moveDirection; //The current direction the snake is moving in

        /// <summary>Get the snake's head segment</summary>
        internal SnakeEntitySegment Head => segments[0];

        /// <summary>Get the X position of the snake's head segment</summary>
        internal int HeadX => segments[0].X;

        /// <summary>Get the Y position of the snake's head segment</summary>
        internal int HeadY => segments[0].Y;

        /// <summary>Get wether the snake should be fully redrawn</summary>
        internal bool FullRedraw => fullRedraw;

        /// <summary>Gets the snake's primary color</summary>
        internal ConsoleColor PrimaryColor => primaryColor;

        internal SnakeEntity(int initialSize, int headX, int headY, ConsoleColor color, Direction moveDirection)
        {
            if(initialSize < minimumSnakeSize) //check that initial size is not less then min
                throw new ArgumentOutOfRangeException(nameof(initialSize) + " cannot be less then " + minimumSnakeSize, nameof(initialSize));

            //TODO: validate that the snake fits in the play area

            this.moveDirection = moveDirection;
            this.primaryColor = color;

            //create segments
            segments = new List<SnakeEntitySegment>();
            segments.Add(new SnakeEntitySegment(headX, headY, SegmentType.Head, this));
            for (int i = 1; i < initialSize; i++) {
                int segmentX = headX, segmentY = headY;
                switch (moveDirection) {
                    case Direction.Up: segmentY += i; break;
                    case Direction.Down: segmentY -= i; break;
                    case Direction.Left: segmentX += i; break;
                    case Direction.Right: segmentX -= i; break;
                }
                segments.Add(new SnakeEntitySegment(segmentX, segmentY, i != initialSize - 1 ? SegmentType.Body : SegmentType.Tail, this)); 
            }
        }

        internal void Update()
        {
            //move snake
        }

        /// <summary>Draws the snake in the console at its current location</summary>
        internal void Draw()
        {
            for (int i = 0; i < segments.Count; i++)
                segments[i].Draw();
            fullRedraw = false;
        }

        /// <summary>Attempts to move the snake in the given direction by the specified amount</summary>
        /// <param name="direction">The <see cref="Direction"/> to move the snake</param>
        /// <param name="moveAmount">The amount to move the snake by</param>
        internal void Move(Direction direction, int moveAmount = 1)
        {
            //calculate the head's new position based on the direction
            int newHeadX = segments[0].X;
            int newHeadY = segments[0].Y;
            switch (direction) {
                case Direction.Up: newHeadY -= moveAmount; break;
                case Direction.Down: newHeadY += moveAmount; break;
                case Direction.Left: newHeadX -= moveAmount; break;
                case Direction.Right: newHeadX += moveAmount; break;
            }

            //check if the new position would hit it self //TODO: this is temporary for testing, //TODO: check colisions, if head colides with self or other 
            if(Utils.IsWithinConsoleBuffer(newHeadX, newHeadY) == false || Intersects(newHeadX, newHeadY) == true) 
                return;

            //set the posiition of each segemnt to the on 'infromt' of it
            for (int i = segments.Count - 1; i > 0; i--)
                if (i - moveAmount < 0) { //if this segment cannot be moved to an other segment move it to head and then move the rest of the way
                    int x = segments[0].X, y = segments[0].Y; //'move' the segment to the old head position
                    switch (direction) { //move the segment in the move direction, removing the segments 'distamce' from the head
                        case Direction.Up: y -= moveAmount - i; break;
                        case Direction.Down: y += moveAmount - i; break;
                        case Direction.Left: x -= moveAmount - i; break;
                        case Direction.Right: x += moveAmount - i; break;
                    }
                    segments[i].SetPosition(x, y);
                } else segments[i].SetPosition(segments[i - moveAmount]); //just move the segment to the segment infront of it

            segments[0].SetPosition(newHeadX, newHeadY);

            if(moveAmount > 1) fullRedraw = true;
        }

        /// <summary>Grows the length of the snake by one segment</summary>
        internal void Grow()
        {
            int tailIdx = segments.Count - 1;
            int x = segments[tailIdx].X;
            int y = segments[tailIdx].Y;

            //move tail to its last position
            segments[tailIdx].ResetPosition();

            //add new segment at tail position
            segments.Insert(tailIdx, new SnakeEntitySegment(x, y, SegmentType.Body, this));
        }

        /// <summary>Checks if the given X/Y location intersects with the snake</summary>
        /// <param name="x">The X location to check</param>
        /// <param name="y">The Y location to check</param>
        /// <returns>True if the location intersects with the snake, false if notS</returns>
        internal bool Intersects(int x, int y)
        {
            for (int i = 0; i < segments.Count; i++)
                if(segments[i].Intersects(x, y) == true)
                    return true;
            return false;
        }

        /// <summary>Checks if the snake intersects with the specified 'rectangle'</summary>
        /// <param name="left">The left most point of the rectangle</param>
        /// <param name="top">The top most point of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>True if the given 'rectangle' intersects with the snake</returns>
        internal bool Intersects(int left, int top, int width, int height)
        {
            for (int i = 0; i < segments.Count; i++)
                if(segments[i].Intersects(left, top, width, height) == true)
                    return true;
            return false;
        }
    }
}