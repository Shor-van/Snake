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
            DrawDebugLine("LT:" + FormatToFourPlace(gameInstance.LastLoopTime.TotalMilliseconds) + " UT:" + FormatToFourPlace(gameInstance.LastUpdateTime.TotalMilliseconds) + " DT:" + FormatToFourPlace(gameInstance.LastDrawTime.TotalMilliseconds) + " SDT:" + FormatToFourPlace(gameInstance.LastScreenDrawTime.TotalMilliseconds) + " FDT:" + FormatToFourPlace(gameInstance.LastFinalizeDrawTime.TotalMilliseconds), ref line, drawBuffer);
            DrawDebugLine("ST:" + FormatToFourPlace(gameInstance.LastSleepTime.TotalMilliseconds) + " LTS:" + FormatToFourPlace(gameInstance.LastTargetSleep) + " GT:" + FormatToFourPlace(gameTime.ElapsedGameTime.TotalMilliseconds) + " TTS:" + FormatToFourPlace(gameInstance.TargetTimeStep.TotalMilliseconds), ref line, drawBuffer);
            DrawDebugLine("DGUT:" + FormatToFourPlace(lastDebugUpdate.TotalMilliseconds) + " DGDT:" + FormatToFourPlace(lastDebugDraw.TotalMilliseconds), ref line, drawBuffer);

            //screens

            lastDebugDraw = gameInstance.CurrentTickElapsedTime - elasped;
        }

        /// <summary>Draws a debug line at the given Y postion and increments that value by 1</summary>
        /// <param name="text">The text to draw</param>
        /// <param name="line">A by ref int to the vertical(Y) position where to draw the line</param>
        /// <param name="drawBuffer">The <see cref="DrawBuffer"/> used to draw to the screen</param>
        private void DrawDebugLine(string text, ref int line, DrawBuffer drawBuffer) => drawBuffer.Write(0, line++, text, ConsoleColor.Cyan);

        /// <summary>Formats a double value with excetly 4 decimal places</summary>
        /// <param name="value">The value to format</param>
        /// <returns>A string version of the double with truncated with 4 decimal places</returns>
        private string FormatToFourPlace(double value) => Math.Round(value, 4).ToString("0.0000");
    }
}