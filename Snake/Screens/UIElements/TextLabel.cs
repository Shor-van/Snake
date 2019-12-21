using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Screens.UIElements
{
    public class TextLabel
    {
        private string text; //The text to show
        private int posX, posY; //The top left location of the text label om the screen        
        private ConsoleColor color; //The color to draw the text with

        //Gets and sets
        public string Text { get { return text; } set { text = value; } }
        public int PositionX { get { return posX; } set { posX = value; } }
        public int PositionY { get { return posY; } set { posY = value; } }
        public ConsoleColor Color { get { return color; } set { color = value; } }

        /// <summary>Base constructor</summary>
        /// <param name="text">The text to show</param>
        /// <param name="posX">The left position at witch to draw the text</param>
        /// <param name="posY">The top position at witch to draw the text</param>
        /// <param name="color">The ConsoleColor at witch to draw the text</param>
        public TextLabel(string text, int posX, int posY, ConsoleColor color)
        {
            this.text = text;
            this.posX = posX;
            this.posY = posY;
            this.color = color;
        }

        /// <summary>Measures the size of the string, Note that its not in pixels but in tiles</summary>
        /// <returns>A array that contains how much tile space the string takes in the window (0) width (1)height</returns>
        public int[] MeasureSize()
        {
            return new int[] { text.Length, 1 };
        }

        /// <summary> Does nothing</summary>
        public void Update() { }

        /// <summary>Draws the textlabel</summary>
        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = Program.backgroundColor;
            Console.SetCursorPosition(posX, posY);
            
            Console.Write(text);

            Console.ResetColor();
        }
    }
}
