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
        private TimeSpan targetTimeStep; //the target time between each game tick
        private readonly int targetTicksPerSecond = 60; //the target number of update ticks per second
        private readonly List<Screen> screens; //holds a list of all active game screens
        private readonly Stopwatch gameTimer; //a stopwatch used to keep track of the current loop time
        private readonly GameTime gameTime; //holds data about the games overall runtime

        internal GameSnake() {
            screens = new List<Screen>();
            gameTime = new GameTime();
            gameTimer = new Stopwatch();
            targetTimeStep = TimeSpan.FromMilliseconds(1000 / targetTicksPerSecond);
        }

        /// <summary>Initalizes the game, sets up the screens</summary>
        private void Initalize()
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Title = "Snake";

            ShowScreen(new MenuScreen(this));
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

            //update gane time
            gameTime.ElapsedGameTime = gameTimer.Elapsed;
            gameTime.TotalGameTime += gameTimer.Elapsed;

            //restart set timer
            gameTimer.Restart();
            
            //update -> draw
            Update(gameTime);
            Draw(gameTime);
        }

        /// <summary>Execute's the update part of the game's loop</summary>
        ///<param name="gameTime">The object that holds info about the game's run time</param>
        private void Update(GameTime gameTime)
        {
            //Check keyboard inputs
            if (Console.KeyAvailable == true || Keyboard.GetIgnoreInput())
                Keyboard.Update(gameTime);

            //process input for screen, if we have
            if(screens.Count > 0) screens[0]?.HandleInput(gameTime);
            
            for (int i = 0; i < screens.Count; i++) //update all active screens
                screens[i].Update(gameTime);

            Keyboard.Clear();
        }

        /// <summary>Executes the draw part of the game's loop</summary>
        ///<param name="gameTime">The object that holds info about the game's run time</param>
        private void Draw(GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++) //draw all acrive screens
                screens[i].Draw(gameTime);
        }

        /// <summary>Shows the specified screen</summary>
        /// <param name="screen">The <see cref="Screen"/> to show</param>
        /// <param name="makePrimary">whether to make the screen as the primary screen(the one that recieves input)</param>
        /// <param name="removeCurrentScreen">whether to remove the current <see cref="Screen"/> from <see cref="screens"/></param>
        internal void ShowScreen(Screen screen, bool makePrimary = true, bool removeCurrentScreen = true)
        {
            if(screen == null) //check that screen is not null
                throw new ArgumentNullException(nameof(screen) + " cannot be null", nameof(screen));

            if (makePrimary == true) //if make screen primary, just use the MakeScreenPrimary function
                { MakeScreenPrimary(screen, removeCurrentScreen); return; }

            //if the screen is already being shown and we are not making it primary just return
            if(screens.Contains(screen) == true) return;

            //if removeCurrentScreen and we have atleast 1 screen
            if(removeCurrentScreen == true && screens.Count > 0)
                screens.RemoveAt(0);

            //initalize and add the screen
            screen.Initalize();
            screens.Add(screen);
            screen.JustSwitchedTo = true;
        }

        /// <summary>Make the specified screen as the primary screen, if the screen is not in the list of <see cref="screens"/> it will be added</summary>
        /// <param name="screen">The screen to make primary</param>
        /// <param name="removeCurrentScreen">whether to remove the current <see cref="Screen"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void MakeScreenPrimary(Screen screen, bool removeCurrentScreen = true)
        {
            if(screen == null) //check that screen is not null
                throw new ArgumentNullException(nameof(screen) + " cannot be null", nameof(screen));

            //if we have a current screen either call screen OnScreenSwitchFrom or if removeCurrentScreen ia true remove it
            if(screens.Count > 0 && screens[0] != null)
                if(removeCurrentScreen == true) screens.RemoveAt(0); else screens[0].OnScreenSwitchFrom();

            if (screens.Contains(screen) != true) //if the screen is not in the screens list, just add it at the start
                { screen.Initalize(); screens.Insert(0, screen); screen.JustSwitchedTo = true; return; }

             //if the screen is already at index 0 do nothing
            if(screen == screens[0]) return;
            
            screens.Remove(screen);
            screens.Insert(0, screen);
            screen.JustSwitchedTo = true;
        }

        /// <summary>Removes the specified screen</summary>
        /// <param name="screen">The <see cref="Screen"/> to remove</param>
        /// <exception cref="ArgumentException"></exception>
        private void RemoveScreen(Screen screen)
        {
            if(screens.Contains(screen) == false)
                throw new ArgumentException("The given screen is not in the list of game screens", nameof(screen));
            screens.Remove(screen);
        }

        /// <summary>Exits the game</summary>
        internal void Exit() => isExiting = true;
    }
}