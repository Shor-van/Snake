using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Screens.UIElements
{
    //Handles the selection arrow
    public class Arrow
    {
        ConsoleColor color; //The color of the arrow
        int posX, posY; //The location on the screen
        bool invert; //Weather the arrow is inverted

        //Gets and sets
        public ConsoleColor Color { get { return color; } set { color = value; } }
        public int PositionX { get { return posX; } set { posX = value; } }
        public int PositionY { get { return posY; } set { posY = value; } }

        /// <summary>Base construcor</summary>
        /// <param name="pColor">The color at wicth to draw the arrow at</param>
        /// <param name="pPosX">The X location at witch to draw the arrow</param>
        /// <param name="pPosY">The Y location at witch to draw the arrow</param>
        /// <param name="pInvert">Weather to draw the arrow inverted</param>
        public Arrow(ConsoleColor pColor, int pPosX, int pPosY,bool pInvert)
        {
            color = pColor;
            posX = pPosX;
            posY = pPosY;
            invert = pInvert;
        }

        /// <summary>Moves the arrow to the spesified location on the screen</summary>
        /// <param name="newPosX">The new X location to move the arrow</param>
        /// <param name="newPosY">The new Y location to move the arrow</param>
        public void Update(int newPosX,int newPosY)
        {
            Console.MoveBufferArea(posX, posY, 2, 1, newPosX, newPosY);
            posX = newPosX;
            posY = newPosY;
        }

        /// <summary>Draws the arrow</summary>
        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = Program.backgroundColor;
            Console.SetCursorPosition(posX,posY);
            
            if (invert)
                Console.Write("<-");           
            else           
                Console.Write("->");

            Console.ResetColor();
        }
    }
}
