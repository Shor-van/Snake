using System;
using System.Collections.Generic;

namespace Snake.Entities
{
    /// <summary>A list of move directions</summary>
    internal enum Direction { Up = 0, Down = 1, Left = 2, Right = 3 }

    /// <summary>Represents a snake entity</summary>
    internal sealed class Snake
    {
        internal const int minimumSnakeSize = 1; //then minimum a snake can be
        internal const char headSegment = 'H'; //the char used for the snake head segment
        internal const char bodySegment = 'B'; //the char used for the snake boday segment
        internal const char tailSegment = 'T'; //the char used for the snake tail segment

        private readonly List<SnakeSegment> segments; //The snake's segments
        private Direction moveDirection; //The current direction the snake is moving in
        private int lastTailX, lastTailY; //The last X and Y location of the tail

        /// <summary>Get the X position of the snake's head segment</summary>
        internal int HeadX => segments[0].X;

        /// <summary>Get the Y position of the snake's head segment</summary>
        internal int HeadY => segments[0].Y;

        internal Snake(int initialSize, int headX, int headY, Direction moveDirection)
        {
            if(initialSize < minimumSnakeSize) //check that initial size is not less then min
                throw new ArgumentOutOfRangeException(nameof(initialSize) + " cannot be less then " + minimumSnakeSize, nameof(initialSize));

            //TODO: validate headX and headY

            segments = new List<SnakeSegment>();
            segments.Add(new SnakeSegment(headX, headY, SegmentType.Head));
            for (int i = 1; i < initialSize; i++)
                segments.Add(new SnakeSegment(headX, headY + i, i != initialSize - 1 ? SegmentType.Body : SegmentType.Tail));
            this.moveDirection = moveDirection;
        }

        internal void Update()
        {

        }
    
        internal void Draw()
        {
            int tailIdx = segments.Count - 1; //see if we need to 'clear' the tail
            if(segments[tailIdx].X != lastTailX || segments[tailIdx].Y != lastTailY)
            {
                Console.SetCursorPosition(lastTailX, lastTailY);
                Console.Write(' ');
                lastTailX = segments[tailIdx].X;
                lastTailY = segments[tailIdx].Y;
            }

            for (int i = 0; i < segments.Count; i++)
                segments[i].Draw();
        }

        internal void Move(Direction direction)
        {
            //calculate the head's new position based on the direction
            int hX = segments[0].X;
            int hY = segments[0].Y;
            switch (direction)
            {
                case Direction.Up: hY -= 1; break;
                case Direction.Down: hY += 1; break;
                case Direction.Left: hX -= 1; break;
                case Direction.Right: hX += 1; break;
            }

            //check if the new position would hit it self //TODO: this is temporary for testing 
            if(Intersects(hX, hY) == true) return;

            //get the tail is(used to 'clear')
            int tailIdx = segments.Count - 1;
            lastTailX = segments[tailIdx].X;
            lastTailY = segments[tailIdx].Y;
            
            //set the posiition of each segemnt to the on 'infromt' of it
            for (int i = segments.Count - 1; i > 0; i--)
                segments[i].SetPosition(segments[i - 1]);

            segments[0].SetPosition(hX, hY);
        }

        /// <summary>Grows the length of the snake by one, </summary>
        internal void Grow()
        {
            int tailIdx = segments.Count - 1;
            int x = segments[tailIdx].X;
            int y = segments[tailIdx].Y;

            //move tail to is last position
            segments[tailIdx].SetPosition(lastTailX, lastTailY);

            segments.Insert(tailIdx, new SnakeSegment(x, y, SegmentType.Body));
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
    }
}