using System;
using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary></summary>
    internal sealed class DebugScreen : Screen
    {
        private TimeSpan lastDebugUpdate; //the time it took to update the debug screen
        private TimeSpan lastDebugDraw; //the time it took to draw the debug screen

        internal DebugScreen(GameSnake gameInstance) : base(gameInstance) { }

        protected override void InitalizeScreen()
        {
            
        }

        protected override void LayoutScreen()
        {
            
        }

        internal override void HandleInput(GameTime gameTime)
        {
            
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            TimeSpan elapsed = gameInstance.CurrentTickElapsedTime;
            lastDebugUpdate = gameInstance.CurrentTickElapsedTime - elapsed;
        }

        protected override void DrawScreen(DrawBuffer drawBuffer, GameTime gameTime)
        {
            TimeSpan elasped = gameInstance.CurrentTickElapsedTime; int line = 0;

            //base data
            DrawDebugLine(gameInstance.GetType().Assembly.GetName().Name + " V:" + typeof(DebugScreen).Assembly.GetName().Version + " FPS:" + "-" + " TPS:" + "-" + " CurrentScreen:" + (gameInstance.PrimaryScreen != null ? gameInstance.PrimaryScreen.GetType().Name : "NULL"), ref line, drawBuffer);
            DrawDebugLine("LT:" + gameInstance.LastLoopTime.TotalMilliseconds + " UT:" + gameInstance.LastUpdateTime.TotalMilliseconds + " DT:" + gameInstance.LastDrawTime.TotalMilliseconds + " FDT:" + gameInstance.LastFinalizeDrawTime.TotalMilliseconds + " GT:" + gameTime.ElapsedGameTime.TotalMilliseconds + " TTS:" + gameInstance.TargetTimeStep.TotalMilliseconds, ref line, drawBuffer);
            DrawDebugLine("DGUT:" + lastDebugUpdate.TotalMilliseconds + " DGDT:" + lastDebugDraw.TotalMilliseconds, ref line, drawBuffer);

            //screens

            lastDebugDraw = gameInstance.CurrentTickElapsedTime - elasped;
        }

        /// <summary>Draws a debug line at the given Y postion and increments that value by 1</summary>
        /// <param name="text">The text to draw</param>
        /// <param name="line">A by ref int to the vertical(Y) position where to draw the line</param>
        /// <param name="drawBuffer">The <see cref="DrawBuffer"/> used to draw to the screen</param>
        private void DrawDebugLine(string text, ref int line, DrawBuffer drawBuffer) => drawBuffer.Write(0, line++, text, ConsoleColor.Cyan);
    }
}