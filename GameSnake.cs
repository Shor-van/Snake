using System;
using System.Diagnostics;
using System.Collections.Generic;

using Snake.Screens;
using Snake.Utilities;
using System.Text;

namespace Snake
{
    internal sealed class GameSnake
    {
        private bool isExiting; //whether the game is exiting
        private bool isRunning; //indicates that Run() has been called
        private TimeSpan targetTimeStep; //the target time between each game tick
        private readonly int targetTicksPerSecond = 60; //the target number of update ticks per second
        private readonly List<Screen> screens; //holds a list of all active game screens
        private readonly DebugScreen debugScreen; //A referance to the debug screen
        private readonly Stopwatch gameTimer; //a stopwatch used to keep track of the current loop time
        private readonly GameTime gameTime; //holds data about the games overall runtime
        private DrawBuffer drawBuffer; //the buffer used to draw the screen

        private TimeSpan lastLoop; //the time it took to process the last loop
        private TimeSpan lastUpdate; //the time it took to process the last update
        private TimeSpan lastDraw; //the time it took to process the last draw
        private TimeSpan lastFinalizeDraw; //the time it took to process the last finalize draw call
        private TimeSpan lastSleepTime; //the time main thread spent sleeping the the last tick
        private double lastTargetSleep; //the target sleep time for the last loop

        /// <summary>Get the time it took to process the last game tick</summary>
        internal TimeSpan LastLoopTime => lastLoop;

        /// <summary>Get the time it took to process the last update call</summary>
        internal TimeSpan LastUpdateTime => lastUpdate;

        /// <summary>Get the time it took to process the last draw call</summary>
        internal TimeSpan LastDrawTime => lastDraw;

        /// <summary>Get the time it took to process the last finalize draw call</summary>
        internal TimeSpan LastFinalizeDrawTime => lastFinalizeDraw;

        /// <summary>Get the time it took to process all the last screen draw calls, note this is the same as, <see cref="LastDrawTime"/> - <see cref="LastFinalizeDrawTime"/></summary>
        internal TimeSpan LastScreenDrawTime => lastDraw - lastFinalizeDraw;

        /// <summary>Get the time main thread spent sleeping the the last tick</summary>
        internal TimeSpan LastSleepTime => lastSleepTime;

        /// <summary>Get the target sleep time for the last loop</summary>
        internal double LastTargetSleep => lastTargetSleep;

        /// <summary>Get the time that has elapsed so far since the start of this game tick</summary>
        internal TimeSpan CurrentTickElapsedTime => gameTimer.Elapsed;

        /// <summary>Get or set the target time step(time between ticks)</summary>
        internal TimeSpan TargetTimeStep { get => targetTimeStep; set => targetTimeStep = value; }

        /// <summary>Get the current primary screen, null if there is no primary screen</summary>
        internal Screen PrimaryScreen => screens.Count > 0 && screens[0] != null ? screens[0] : null;

        internal GameSnake() {
            screens = new List<Screen>();
            debugScreen = new DebugScreen(this);
            gameTime = new GameTime();
            gameTimer = new Stopwatch();

            double targetTime = Math.Round(1000d / targetTicksPerSecond, 4);
            targetTimeStep = TimeSpan.FromTicks((long)(targetTime * TimeSpan.TicksPerMillisecond));
        }

        /// <summary>Initalizes the game, sets up the screens</summary>
        private void Initalize()
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Title = "Snake";

            drawBuffer = new DrawBuffer(0, 0, (short)Console.BufferWidth, (short)Console.BufferHeight, Encoding.Unicode, ConsoleColor.Gray, ConsoleColor.Black);

            ShowScreen(new MenuScreen(this));
            ShowScreen(debugScreen, false, false);
            debugScreen.Hidden = true;
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
            lastLoop = gameTimer.Elapsed;
            if (lastLoop < targetTimeStep) { 
                double taregtSleep = targetTimeStep.TotalMilliseconds - lastLoop.TotalMilliseconds; 
                SleepHelper.SleepForNoMoreThan(taregtSleep); 
                lastSleepTime = gameTimer.Elapsed - lastLoop;
                lastTargetSleep = taregtSleep;
            } else lastSleepTime = TimeSpan.Zero;

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
            TimeSpan elapsed = gameTimer.Elapsed;
            if (Console.KeyAvailable == true || Keyboard.GetIgnoreInput())
                Keyboard.Update(gameTime);

            //Check debug key
            if (Keyboard.IsNewKeyPress(ConsoleKey.F1) == true) debugScreen.Hidden = !debugScreen.Hidden;

            //process input for screen, if we have
            if(screens.Count > 0) screens[0]?.HandleInput(gameTime);
            
            for (int i = 0; i < screens.Count; i++) //update all active screens
                screens[i].Update(gameTime);

            Keyboard.Clear();
            lastUpdate = gameTimer.Elapsed - elapsed;
        }

        /// <summary>Executes the draw part of the game's loop</summary>
        ///<param name="gameTime">The object that holds info about the game's run time</param>
        private void Draw(GameTime gameTime)
        {
            TimeSpan elapsedStart = gameTimer.Elapsed;
            for (int i = 0; i < screens.Count; i++) //draw all acrive screens
                screens[i].Draw(drawBuffer, gameTime);

            TimeSpan elapsedFinalDraw = gameTimer.Elapsed;
            drawBuffer.FiinalizeDrow(true);
            lastFinalizeDraw = gameTimer.Elapsed - elapsedFinalDraw;
            lastDraw = gameTimer.Elapsed - elapsedStart;
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