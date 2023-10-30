using System;

namespace Snake.Screens
{
    /// <summary>Base class for all screen</summary>
    internal abstract class Screen
    {
        protected readonly GameSnake gameInstance; //A referance to the game the screen 'belongs' to
        protected bool justSwitchedTo; //whether the screen was just switched to

        protected Screen(GameSnake gameInstance) => this.gameInstance = gameInstance ?? throw new ArgumentNullException(nameof(gameInstance) + " cannot be null", nameof(gameInstance));

        /// <summary>Called when the screen was just switched to</summary>
        protected virtual void OnScreenSwitchTo() { }

        internal void Update() 
        {
            if (justSwitchedTo == true) //Check if the screen was just switched to
                OnScreenSwitchTo();
            UpdateScreen();
        }

        internal abstract void HandleInput();

        protected abstract void UpdateScreen();

        internal void Draw()
        {
            DrawScreen();

            justSwitchedTo = false;
        }

        protected abstract void DrawScreen();
    }
}