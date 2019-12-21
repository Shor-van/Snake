using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake.Screens.UIElements
{
    //This will handle the text used in the menus 
    public class MenuText
    {
        string text; //The text to show
        int posX, posY; //The top left location on the screen
        ConsoleColor aColor, sColor, wColor; //All the colors to use
        bool active; //Weather the player has this option selected
        bool enable; //Weather the player can select this option

        //Events
        public delegate void BaseEventHandler(object sender, EventArgs e); //Base event handler
        public event BaseEventHandler Selected; //Event triggered when the player presses enter while this option is active
        public event BaseEventHandler HoveredOn; //Event triggered when the player switched to this item/this item bacame active
        public event BaseEventHandler HoveredOff; //Event triggered when the player sweitched off this item/this item when from active to inactive 

        //Gets and sets
        public string Text { get { return text; } set { text = value; } }
        public int PositionX { get { return posX; } set { posX = value; } }
        public int PositionY { get { return posY; } set { posY = value; } }

        public bool Active { get { return active; } set { active = value; } }
        public bool Enable { get { return enable; } set { enable = value; } }

        public ConsoleColor WColor { get { return wColor; } set { wColor = value; }}
        public ConsoleColor SColor { get { return sColor; } set { sColor = value; } }
        public ConsoleColor AColor { get { return aColor; } set { aColor = value; } }

        /// <summary>The base constructor</summary>
        /// <param name="pText">The text to show</param>
        /// <param name="pPosX">The left most location of the text on the screen</param>
        /// <param name="pPosY">The top most location of the text on the screen</param>
        public MenuText(string pText, int pPosX, int pPosY)
        {
            text = pText;
            posX = pPosX;
            posY = pPosY;
            active = false;
            enable = true;
            aColor = wColor = ConsoleColor.White;
            sColor = ConsoleColor.Yellow;
        }

        /// <summary>Measures the size of the string, Note that its not in pixels but in tiles</summary>
        /// <returns>A array that contains how much tile space the string takes in the window (0) width (1)height</returns>
        public int[] MeasureSize()
        {
            return new int[] { text.Length,1 };
        }

        //Event handlers
        /// <summary>Triggered when the player hits enter while this is active</summary>
        /// <param name="e">Event args</param>
        protected void OnSelected(EventArgs e)
        {
            if (Selected != null)
                Selected(this, e);
        }

        /// <summary>Triggered when this item becomes active</summary>
        /// <param name="e">Event args</param>
        protected void OnHoveredOn(EventArgs e)
        {
            if (HoveredOn != null)
                HoveredOn(this, e);
        }

        /// <summary>Triggered when this item becomes inactive</summary>
        /// <param name="e">Event args</param>
        protected void OnHoveredOff(EventArgs e)
        {
            if (HoveredOff != null)
                HoveredOff(this, e);
        }

        //Loop
        /// <summary>Updates the MenuText</summary>
        public void Update()
        {
            if (active && enable && (aColor == wColor || aColor == ConsoleColor.DarkGray))
            {
                aColor = sColor;
                OnHoveredOn(EventArgs.Empty);
            }
            else if (!active && aColor == sColor)
            {
                aColor = wColor;
                OnHoveredOff(EventArgs.Empty);
            }

            if (!active)
            {
                if (!enable)
                    aColor = ConsoleColor.DarkGray;
                else
                    aColor = wColor;
            }

            if (active && enable && Keyboard.IsKeyPressed(ConsoleKey.Enter))
                OnSelected(EventArgs.Empty);
        }

        /// <summary>Draws the MenuText</summary>
        public void Draw()
        {
            Console.ForegroundColor = aColor;
            Console.BackgroundColor = Program.backgroundColor;
            Console.SetCursorPosition(posX, posY);
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
