using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Snake.Utils;
using Snake.Input;

namespace Snake
{
    /// <summary>All the states of the game</summary>
    public enum GameState
    {
        IntroState,
        MenuState,
        PlayState,
        GameOverState,
        HighScoreState,
        HelpState,
    }
    public static class Program
    {
        //Game variables
        public static bool isExiting = false; //Weather the game is exiting
        public static bool switchingScreen = true; //Weather the screen is changing
        public static ConsoleColor backgroundColor; //the background color of the game
        public static GameState gameState = GameState.IntroState; //The "state" of the game, used to see witch screen to draw
        public static float lastLoopTime; //The time that the last game loop took to complete
        public static bool showDebug = false; //Weather or not to draw the debug screen
        public static bool gameWon = false; //Weather the player won the game
        private static bool sizeChanged = false; //Weather the window size has changed 
        private static int viewWidth, viewHeight;  //The width and height of the game window
        
        //GameTime
        public static Stopwatch gameTime;

        //Screens

        /// <summary>The entry point of the program</summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                //Setup game
                Initalize();

                //The game loop will be here, all Draw and Upadate mathods will be called from here
                //in the form of check input then update objects then draw screen
                while (true)                
                    Tick();                
            }
            catch (Exception e)
            {
                CrashReporter.CreateCrashReport(e, new string[] { "This crash heppened in a area that was not monitored." });
            }
        }

        /// <summary>Initalizes the game, setsup the screens</summary>
        private static void Initalize()
        {
        }

        /// <summary>The main game loop, Update -> Draw -> Reset</summary>
        private static void Tick() 
        {
            gameTime.Restart();
            if (!isExiting)
            {
                //Update
                Update();

                //Draw
                Draw();

                //Reset
                Keyboard.Clear();
                sizeChanged = false;
                System.Threading.Thread.Sleep(0);
            }
            else
            {
                Environment.Exit(0);
            }
            lastLoopTime = (float)gameTime.Elapsed.TotalMilliseconds;
        }

        /// <summary>The Update stage of the loop, only the current screen is updated witch is determined by the GameState</summary>
        private static void Update()
        {
        }

        /// <summary>The Draw stage of the loop,  only the current screen is drawn witch is determined by the GameState</summary>
        private static void Draw()
        {
        }

        /// <summary>Sets up a new game</summary>
        public static void SetUpNewGame()
        {
            switchingScreen = true;
        }

        /// <summary>Changes the size of the window sisze</summary>
        /// <param name="width">The width of the window in tiles</param>
        /// <param name="height">The height of the window in tiles</param>
        public static void ChangeWindowSize(int width, int height)
        {
            viewWidth = width;
            viewHeight = height;

            Console.SetWindowSize(viewWidth, viewHeight);
            Console.SetBufferSize(viewWidth, viewHeight);

            sizeChanged = true;
        }

        /// <summary>Gets weather the size of the window was changewd in the last loop</summary>
        /// <returns>True if the window size canged in the last game loop else false</returns>
        public static bool WindowSizeChanged() { return sizeChanged; }
    }
}
