using System;

namespace Snake.Screens
{
    /// <summary></summary>
    internal sealed class GameScreen : Screen
    {
        internal GameScreen(GameSnake gameInstance)
        : base(gameInstance)
        {

        }

        internal override void HandleInput()
        {
            throw new NotImplementedException();
        }
        
        protected override void UpdateScreen()
        {
            throw new NotImplementedException();
        }

        protected override void DrawScreen()
        {
            throw new NotImplementedException();
        }
    }
}
