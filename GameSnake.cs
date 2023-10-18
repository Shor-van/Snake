using System;
using System.Diagnostics;
using System.Collections.Generic;

using Snake.Screens;

namespace Snake
{
    internal sealed class GameSnake
    {
        private readonly List<Screen> screens; //holds a list of all active game screens
        private Screen primaryScreem; //a referance to the 'main' active screen
        private bool isExiting; //whether the game is exiting
        private Stopwatch gameTimer; //a stopwatch used to keep track of the current loop time

        internal GameSnake()
        {

        }

        /// <summary>Initalizes the game, sets up the screens</summary>
        private void Initalize()
        {
        }

        /// <summary>Initilaizes the game and runs the main game loop, Update -> Draw -> Reset</summary>
        internal void Run()
        {
            Initalize(); //initialize the game

            while (isExiting == false) //run game loop
                Tick();

            Environment.Exit(0);
        }

        /// <summary>Executes one complete game tick; Update -> Draw -> Reset</summary>
        private void Tick()
        {
            Update();

            Draw();
        }

        /// <summary>Execute's the update part of the game's loop</summary>
        private void Update()
        {
            primaryScreem.HandleInput(); //process input for screen
            primaryScreem.Update(); //update scr

            for (int i = 0; i < screens.Count; i++)
                if(screens[i] != primaryScreem)
                    screens[i].Update();
        }

        /// <summary>Executes the draw part of the game's loop</summary>
        private void Draw()
        {
            primaryScreem.Draw();
            for (int i = 0; i < screens.Count; i++)
                if(screens[i] != primaryScreem)
                    screens[i].Draw();
        }

        internal void ShowScreen(Screen screen)
        {
            
        }

        /// <summary>Exits the game</summary>
        internal void Exit() => isExiting = true;
    }
}