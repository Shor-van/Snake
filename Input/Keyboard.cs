using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Input
{
    //Will handle all the input logic
    public static class Keyboard
    {
        private static ConsoleKeyInfo currentKeyInfo; //The current key info
        private static ConsoleKeyInfo lastKeyInfo; //The last loops key info
        private static ConsoleKey lastPressedKey; //The key that was pressed in the last game loop
        
        private static bool ignoreInput = false; //Weather the keyboard will read key presses
        private static float timeToIgnore = 0f; //The time in ms
        private static float elepsedTimeToIgnore = 0f; //The time that has elepsed in ms

        /// <summary>Updates the keyboard handler, Gets the current state of the keys</summary>
        public static void Update()
        {
            if (ignoreInput == false)
                currentKeyInfo = Console.ReadKey(true);
            else
            {
                Clear();

                //If a key has been pressed get it and ignore it
                if (Console.KeyAvailable)
                    Console.ReadKey(true);

                if (timeToIgnore > 0f)//Check if the "ignoreInput" state should be removed if the set time passes
                {
                    elepsedTimeToIgnore += Program.lastLoopTime;
                    if (elepsedTimeToIgnore >= timeToIgnore)
                    {
                        elepsedTimeToIgnore = 0f;
                        timeToIgnore = 0f;
                        ignoreInput = false;
                    }
                }
            }
        }

        /// <summary>Checks if the spesified key is pressed</summary>
        /// <param name="key">The key to check if it is pressed</param>
        /// <returns>True if the key is pressed</returns>
        public static bool IsKeyPressed(ConsoleKey key)
        {
            return currentKeyInfo.Key == key;
        }

        /// <summary>Checks if the spesified key is pressed and that it was not pressed in the last loop</summary>
        /// <param name="key">The key to check if it is pressed</param>
        /// <returns>True if the key is pressed in this loop and not pressed in the last</returns>
        public static bool IsNewKeyPress(ConsoleKey key)
        {
            if (lastPressedKey != key && currentKeyInfo.Key == key)
                return true;
            return false;
        }

        /// <summary>Checks if that any key is pressed</summary>
        /// <returns>True if a key was pressed else false</returns>
        public static bool IsAnyKeyPressed()
        {
            if (currentKeyInfo.Key != 0)
                return true;
            return false;
        }

        /// <summary>Gets the currently pressed key</summary>
        /// <returns>The currently pressed key</returns>
        public static ConsoleKey GetPressedKey() { return currentKeyInfo.Key; }

        /// <summary>Ignores keyboard input for the specific time in ms</summary>
        /// <param name="timeToIgnore">The time to ignore input in ms</param>
        public static void IgnoreInputFor(float tToIgnore) 
        {
            ignoreInput = true;
            timeToIgnore = tToIgnore;  
        }

        /// <summary>Sets if the keybaord should ignore key presses</summary>
        /// <param name="value">True or false</param>
        public static void SetIgnoreInput(bool value) { ignoreInput = value; }

        /// <summary>Removes all pressed keyys</summary>
        public static void Clear()
        {
            lastPressedKey = currentKeyInfo.Key;
            currentKeyInfo = new ConsoleKeyInfo();
        }

        /// <summary>Debug method used to see the state of ignoreInput</summary>
        /// <returns>The state of ignoreInput</returns>
        public static bool GetIgnoreInput() { return ignoreInput; }

        /// <summary>Debug method used to see the current elepsed time</summary>
        /// <returns>The elepsed time that is used in the ignore for time</returns>
        public static float GetElepsedTime() { return elepsedTimeToIgnore; }

        /// <summary>Debug method used to see how much time that input is being ignored</summary>
        /// <returns>The amount of time that input should be ignored</returns>
        public static float GetIgnoreTime() { return timeToIgnore; }
    }
}
