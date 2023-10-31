using System;
using System.Diagnostics;
using System.Collections.Generic;

using Snake.Screens;
using Snake.Utilities;

namespace Snake
{
    internal sealed class GameSnake
    {
        private bool isExiting; //whether the game is exiting
        private bool isRunning; //indicates that Run() has been called
        private Stopwatch gameTimer; //a stopwatch used to keep track of the current loop time
        private GameTime gameTime; //holds data about the games overall runtime
        private List<Screen> screens; //holds a list of all active game screens
        private Screen primaryScreem; //a referance to the 'main' active screen
        private TimeSpan targetTimeStep; //the target time between each game tick

        internal GameSnake() { }

        /// <summary>Initalizes the game, sets up the screens</summary>
        private void Initalize()
        {
            screens = new List<Screen>();
            gameTime = new GameTime();
            gameTimer = new Stopwatch();
            targetTimeStep = TimeSpan.FromMilliseconds(16.66667);

            isExiting = false;
        }

        /// <summary>Initilaizes the game and runs the main game loop, Update -> Draw -> Reset</summary>
        internal void Run()
        {
            if(isRunning == true)
                throw new InvalidOperationException("Game is already running");

            Initalize(); //initialize the game

            gameTimer.Start();
            isRunning = true;

            while (isExiting == false) //run game loop
                Tick();

            Environment.Exit(0);
        }

        /// <summary>Executes one complete game tick; Update -> Draw -> Reset</summary>
        private void Tick()
        {
            //Sleep if we need to maintain a fixed rate
            TimeSpan lastLoop = gameTimer.Elapsed;
            if(lastLoop < targetTimeStep)
                System.Threading.Thread.Sleep(targetTimeStep - lastLoop);

            gameTime.ElapsedGameTime = gameTimer.Elapsed;
            gameTime.TotalGameTime += gameTimer.Elapsed;

            gameTimer.Restart();
            
            Update(gameTime);
            Draw(gameTime);
        }

        /// <summary>Execute's the update part of the game's loop</summary>
        private void Update(GameTime gameTime)
        {
            primaryScreem.HandleInput(); //process input for screen
            primaryScreem.Update(); //update screen

            for (int i = 0; i < screens.Count; i++)
                if(screens[i] != primaryScreem)
                    screens[i].Update();
        }

        /// <summary>Executes the draw part of the game's loop</summary>
        private void Draw(GameTime gameTime)
        {
            primaryScreem.Draw();
            for (int i = 0; i < screens.Count; i++)
                if(screens[i] != primaryScreem)
                    screens[i].Draw();
        }

        /// <summary>Shows the specified screen</summary>
        /// <param name="screen">The <see cref="Screen"/> to show</param>
        internal void ShowScreen(Screen screen)
        {
            
        }

        /// <summary>Removes the specified screen</summary>
        /// <param name="screen">The <see cref="Screen"/> to remove</param>
        /// <exception cref="ArgumentException"></exception>
        internal void RemoveScreen(Screen screen)
        {
            if(screens.Contains(screen) == false)
                throw new ArgumentException("The given screen is not in the list of game screens", nameof(screen));
            screens.Remove(screen);
        }

        /// <summary>Exits the game</summary>
        internal void Exit() => isExiting = true;
    }
}