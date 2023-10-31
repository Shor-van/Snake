using System;

namespace Snake.Utilities
{
    /// <summary>Holds data about the game's overall runtime</summary>
    internal sealed class GameTime
    {
        private TimeSpan totalGameTime; //the total time elapsed since the start of the game
        private TimeSpan elapsedGameTime; //the time elapsed since the last call to update

        /// <summary>Time since the start of the <see cref="GameSnake"/></summary>
        internal TimeSpan TotalGameTime { get => totalGameTime; set => totalGameTime = value; }

        /// <summary>Time since the last call to <see cref="GameSnake.Update"/></summary>
        internal TimeSpan ElapsedGameTime { get => elapsedGameTime; set => elapsedGameTime = value; }

        /// <summary>Gets the total amount of ticks that have elapsed since the start of the game</summary>
        internal long TotalTicks => totalGameTime.Ticks;

        /// <summary>Gets the total amount of time that has elapsed since the start of the game</summary>
        internal double TotalMilliseconds => totalGameTime.TotalMilliseconds;

        /// <summary>Create a <see cref="GameTime"/> instance with a <see cref="totalGameTime"/> and <see cref="elapsedGameTime"/> of 0</summary>
        public GameTime()
        {
            totalGameTime = TimeSpan.Zero;
            elapsedGameTime = TimeSpan.Zero;
        }
    }
}