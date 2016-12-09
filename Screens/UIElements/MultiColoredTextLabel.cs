using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Screens.UIElements
{
    public class MultiColoredTextLabel
    {
        private string text; //The text to show
        private int posX, posY; //The top left location of the text label on screen
        private ConsoleColor baseColor; //The base color to draw the text label in

        //Gets and sets
        public string Text { get { return text; } set { text = value; } }
        public int PositionX { get { return posX; } set { posX = value; } }
        public int PositionY { get { return posY; } set { posY = value; } }
        public ConsoleColor BaseColor { get { return baseColor; } set { baseColor = value; } }

        /// <summary>Base constructor</summary>
        /// <param name="text">The text to show</param>
        /// <param name="posX">The left position at witch to draw the text</param>
        /// <param name="posY">The top position at witch to draw the text</param>
        /// <param name="color">The base ConsoleColor at witch to draw the text</param>
        public MultiColoredTextLabel(string text, int posX, int posY, ConsoleColor baseColor)
        {
            this.text = text;
            this.posX = posX;
            this.posY = posY;
            this.baseColor = baseColor;
        }

        /// <summary>Gets the ConsoleColor that the spesified code represents</summary>
        /// <param name="code">The code to check</param>
        /// <returns>The ConsoleColor that the spesified code represents</returns>
        public static ConsoleColor? GetColorFromCode(string code)
        {
            if (code == "&0")
                return ConsoleColor.Black;
            else if (code == "&1")
                return ConsoleColor.White;
            else if (code == "&2")
                return ConsoleColor.Red;
            else if (code == "&3")
                return ConsoleColor.Green;
            else if (code == "&4")
                return ConsoleColor.Blue;
            else if (code == "&5")
                return ConsoleColor.Yellow;
            else if (code == "&6")
                return ConsoleColor.Cyan;
            else if (code == "&7")
                return ConsoleColor.Magenta;
            else if (code == "&8")
                return ConsoleColor.Gray;
            return null;
        }

        /// <summary>Measures the size of the string, Note that its not in pixels but in tiles</summary>
        /// <returns>A array that contains how much tile space the string takes in the window (0) width (1)height</returns>
        public int[] MeasureSize()
        {
            int visableLenght = 0;
            char[] letters = text.ToArray<char>();
            for (int i = 0; i < letters.Length; i++)
            {
                //Check if is color code
                if (letters[i] == '&' && i + 1 < letters.Length)
                {
                    //Attempt Get color code
                    ConsoleColor? isColorCode = GetColorFromCode(letters[i].ToString() + letters[i + 1].ToString());

                    //if is color code or reset code skip
                    if (isColorCode != null || letters[i + 1] == 'r')
                        i++;
                    else //Just add char to visablelenght
                        visableLenght++;
                }
                else //Just add char to visablelenght
                    visableLenght++;
            }
            return new int[] { visableLenght, 1 };
        }

        /// <summary>Does nothing</summary>
        public void Update() { }

        /// <summary>Draws the MultiColoredTextLabel</summary>
        public void Draw()
        {
            Console.SetCursorPosition(posX, posY);
            char[] letters = text.ToArray<char>();
            ConsoleColor tmpColor = baseColor;
            for (int i = 0; i < letters.Length; i++)
            {
                //Check if is color code
                if (letters[i] == '&' && i + 1 < letters.Length)
                {
                    //Get color code
                    ConsoleColor? newColor = GetColorFromCode(letters[i].ToString() + letters[i + 1].ToString());

                    //if not null set new color
                    if (newColor != null)
                    {
                        tmpColor = (ConsoleColor)newColor;
                        i++;
                    }
                    else if (letters[i + 1] == 'r')//Check if is reset color code
                    {
                        tmpColor = baseColor;
                        i++;
                    }
                    else //just write like normal
                    {
                        Console.ForegroundColor = tmpColor;
                        Console.Write(letters[i]);
                    }
                }
                else //just write like normal
                {
                    Console.ForegroundColor = tmpColor;
                    Console.Write(letters[i]);
                }
            }
            Console.ResetColor();
        }
    }
}
