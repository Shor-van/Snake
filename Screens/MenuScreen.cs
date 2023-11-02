using System;

using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary>Represents the game's main menu screen</summary>
    internal sealed class MenuScreen : Screen
    {
        private const string PlayGame = "Play Game"; //a constant for the play game text, the option that the player selects to play the game
        private const string HighScores = "High Scores"; //a constant for the play game text, the option that the player selects to play the game
        private const string HowToPlay = "How To Play"; //a constant for the play game text, the option that the player selects to play the game
        private const string Exit = "Exit"; //a constant for the play game text, the option that the player selects to play the game

        private SelectionList selection; //the menu selection list

        /// <summary>creates a new instance of <see cref="MenuScreen"/></summary>
        /// <param name="gameInstance">A referance to the games's instance</param>
        internal MenuScreen(GameSnake gameInstance) : base(gameInstance) { }
        
        /// <summary>Initalizes the <see cref="MenuScreen"/> and all its elements</summary>
        protected override void InitalizeScreen()
        {
            selection = new SelectionList(0, 0, new string[] { PlayGame, HighScores, HowToPlay, Exit}, ConsoleColor.White, ConsoleColor.Yellow, "-> ", Alignment.Center);

            selection.Add_SelectedHandlerFor(PlayGame, PlayGame_Selected);
            selection.Add_SelectedHandlerFor(HighScores, HighScores_Selected);
            selection.Add_SelectedHandlerFor(HowToPlay, HowToPlay_Selected);
            selection.Add_SelectedHandlerFor(Exit, Exit_Selected);
        }

        protected override void LayoutScreen()
        {
            selection.X = (Console.WindowWidth / 2) - (selection.Width / 2);
            selection.Y = (Console.WindowHeight / 2) - (selection.Height / 2);
        }

        internal override void HandleInput(GameTime gameTime)
        {
            selection.HandleInput(gameTime);
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            selection.Update(gameTime);
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            selection.Draw(gameTime);
        }

        //Event handlers
        /// <summary>Handles the Play Game selected event, sets up a new game</summary>
        /// <param name="sender">The object that rasied the event</param>
        /// <param name="e">The <see cref="EventArgs"/> object containing info on the rasied event</param>
        private void PlayGame_Selected(object sender, EventArgs e) => gameInstance.ShowScreen(new GameScreen(gameInstance));

        /// <summary>Handles the Highscores selected event, shows the Highscores screen</summary>
        /// <param name="sender">The object that rasied the event</param>
        /// <param name="e">The <see cref="EventArgs"/> object containing info on the rasied event</param>
        private void HighScores_Selected(object sender, EventArgs e)
        {

        }

        /// <summary>Handles the Help selected event, shows the Help screen</summary>
        /// <param name="sender">The object that rasied the event</param>
        /// <param name="e">The <see cref="EventArgs"/> object containing info on the rasied event</param>
        private void HowToPlay_Selected(object sender, EventArgs e)
        {

        }

        /// <summary>Handles the Exit selected event, tells the game to exit</summary>
        /// <param name="sender">The object that rasied the event</param>
        /// <param name="e">The <see cref="EventArgs"/> object containing info on the rasied event</param>
        private void Exit_Selected(object sender, EventArgs e) => gameInstance.Exit();
    }
}