using System;
using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary>Base class for all screen</summary>
    internal abstract class Screen
    {
        protected readonly GameSnake gameInstance; //A referance to the game the screen 'belongs' to
        protected bool justSwitchedTo; //whether the screen was just switched to
        private bool initalized; //whether the screen has been initalized

        /// <summary>Gets wether the <see cref="Screen"/> has been initalized</summary>
        internal bool Initalized => initalized;

        /// <summary>Get or set whether this <see cref="Screen"/> was just switched to, witch means it will call <see cref="Screen.OnScreenSwitchTo"/></summary>
        internal bool JustSwitchedTo { get => justSwitchedTo; set => justSwitchedTo = value; }

        /// <summary>Base constructor for <see cref="Screen"/>, this should never be called outside a dervied class</summary>
        /// <param name="gameInstance">Refrence to the game's main instance</param>
        /// <exception cref="ArgumentNullException">Thrown if gameInstance is null</exception>
        protected Screen(GameSnake gameInstance) => this.gameInstance = gameInstance ?? throw new ArgumentNullException(nameof(gameInstance) + " cannot be null", nameof(gameInstance));

        /// <summary>Initalizes the <see cref="Screen"/> and all of it's objects</summary>
        public void Initalize()
        {
            //Check if screen was aready initalized
            if (initalized == true) return;

            InitalizeScreen();
            initalized = true;
        }

        /// <summary>Initalizes the screen and all of its objects</summary>
        protected abstract void InitalizeScreen();

        /// <summary>Recalulates the positions for all the screens's elements</summary>
        protected abstract void LayoutScreen();

        /// <summary>Called when the screen was just switched to</summary>
        /// <param name="gameTime"></param>
        protected virtual void OnScreenSwitchTo(GameTime gameTime) { }

        /// <summary>Called when the screen was just switched from</summary>
        internal virtual void OnScreenSwitchFrom() { }

        internal void Update(GameTime gameTime) 
        {
            if (justSwitchedTo == true) { //Check if the screen was just switched to
                LayoutScreen();
                OnScreenSwitchTo(gameTime);
            }

            UpdateScreen(gameTime);
        }

        internal abstract void HandleInput(GameTime gameTime);

        protected abstract void UpdateScreen(GameTime gameTime);

        internal void Draw(GameTime gameTime)
        {
            DrawScreen(gameTime);

            justSwitchedTo = false;
        }

        protected abstract void DrawScreen(GameTime gameTime);
    }
}