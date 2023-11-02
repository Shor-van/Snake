using System;

using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary></summary>
    internal sealed class MenuText
    {
        private int x, y; //the X and Y position of the menu text
        private string text; //the text of the menu text
        private bool highlighted; //wether the menu text is highlighted(selceted)
        private ConsoleColor color, highlightedColor; //the colors used to draw the text

        /// <summary></summary>
        internal event EventHandler Selected;

        /// <summary></summary>
        internal event EventHandler OnHighlighted;

        /// <summary></summary>
        internal event EventHandler OnUnhighlighted;

        internal void HandleInput(GameTime gameTime)
        {
            
        }

        internal void Update(GameTime gameTime)
        {

        }

        internal void Draw(GameTime gameTime)
        {

        }
    }
}