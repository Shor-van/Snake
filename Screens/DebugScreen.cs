using System;
using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary></summary>
    internal sealed class DebugScreen : Screen
    {
        internal TimeSpan lastDebugDraw;

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
            
        }

        protected override void DrawScreen(DrawBuffer drawBuffer, GameTime gameTime)
        {
            TimeSpan elasped = gameInstance.CurrentTickElapsedTime;
            int line = 0;
            drawBuffer.Write(0, line++, gameInstance.GetType().Assembly.GetName().Name + " V:" + typeof(DebugScreen).Assembly.GetName().Version, ConsoleColor.Cyan);
            drawBuffer.Write(0, line++, "LT:" + gameInstance.LastLoopTime.TotalMilliseconds + " UT:" + gameInstance.LastUpdateTime.TotalMilliseconds + " DT:" + gameInstance.LastDrawTime.TotalMilliseconds + " FDT:" + gameInstance.LastFinalizeDraw.TotalMilliseconds + " GT:" + gameTime.ElapsedGameTime.TotalMilliseconds, ConsoleColor.Cyan);
            drawBuffer.Write(0, line++, "TTS:" + gameInstance.TargetTimeStep.TotalMilliseconds + " DGUT:" + " DGDT:" + lastDebugDraw.TotalMilliseconds, ConsoleColor.Cyan);
            lastDebugDraw = gameInstance.CurrentTickElapsedTime - elasped;
        }
    }
}