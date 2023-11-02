using System;

namespace Snake.Utilities
{
    //Will handle all the input logic
    internal static class Keyboard
    {
        private static ConsoleKeyInfo currentKeyInfo; //The current key info
        private static ConsoleKeyInfo lastKeyInfo; //The last loops key info
        
        private static bool ignoreInput = false; //Weather the keyboard will read key presses
        private static double timeToIgnore = 0f; //The time in ms
        private static double elepsedTimeToIgnore = 0f; //The time that has elepsed in ms

        /// <summary>Updates the keyboard handler, Gets the current state of the keys</summary>
        internal static void Update(GameTime gameTime)
        {
            if (ignoreInput == false)
                currentKeyInfo = Console.ReadKey(true);
            else
            {
                Clear();

                //If a key has been pressed get it and ignore it
                if (Console.KeyAvailable) Console.ReadKey(true);

                if (timeToIgnore > 0f) //Check if the "ignoreInput" state should be removed if the set time passes
                {
                    elepsedTimeToIgnore += gameTime.ElapsedGameTime.TotalMilliseconds;
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
        internal static bool IsKeyPressed(ConsoleKey key) => currentKeyInfo.Key == key;

        /// <summary>Checks if the spesified key is pressed and that it was not pressed in the last loop</summary>
        /// <param name="key">The key to check if it is pressed</param>
        /// <returns>True if the key is pressed in this loop and not pressed in the last</returns>
        internal static bool IsNewKeyPress(ConsoleKey key) => lastKeyInfo.Key != key && currentKeyInfo.Key == key;

        /// <summary>Checks if that any key is pressed</summary>
        /// <returns>True if a key was pressed else false</returns>
        internal static bool IsAnyKeyPressed() => currentKeyInfo.Key != 0;

        /// <summary>Gets the currently pressed key</summary>
        /// <returns>The currently pressed key</returns>
        internal static ConsoleKey GetPressedKey() => currentKeyInfo.Key;

        /// <summary>Ignores keyboard input for the specific time in ms</summary>
        /// <param name="timeToIgnore">The time to ignore input in ms</param>
        internal static void IgnoreInputFor(float tToIgnore) 
        {
            ignoreInput = true;
            timeToIgnore = tToIgnore;  
        }

        /// <summary>Sets if the keybaord should ignore key presses</summary>
        /// <param name="value">True or false</param>
        internal static void SetIgnoreInput(bool value) => ignoreInput = value;

        /// <summary>Removes all pressed keyys</summary>
        internal static void Clear()
        {
            lastKeyInfo = currentKeyInfo;
            currentKeyInfo = new ConsoleKeyInfo();
        }

        /// <summary>Debug method used to see the state of ignoreInput</summary>
        /// <returns>The state of ignoreInput</returns>
        internal static bool GetIgnoreInput() => ignoreInput;

        /// <summary>Debug method used to see the current elepsed time</summary>
        /// <returns>The elepsed time that is used in the ignore for time</returns>
        internal static double GetElepsedTime() => elepsedTimeToIgnore;

        /// <summary>Debug method used to see how much time that input is being ignored</summary>
        /// <returns>The amount of time that input should be ignored</returns>
        internal static double GetIgnoreTime() => timeToIgnore;
    }
}
