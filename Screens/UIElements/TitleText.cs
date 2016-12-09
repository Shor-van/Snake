using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Screens.UIElements
{
    //Will handle all the drawing and update of the Title Texts
    public class TitleText
    {
        string text; //The text to display
        ConsoleColor color; //The color at witch to draw the text
        int posX, posY; //The top left location at witch to draw the text

        //Gets and set
        public string Text { get { return text; } set { text = value; } }
        public ConsoleColor Color { get { return color; } set { color = value; } }
        public int PositionX { get { return posX; }set { posX = value; } }
        public int PositionY { get { return posY; } set { posY = value; } }
        
        /// <summary>Base construcor</summary>
        /// <param name="pText">The text to draw</param>
        /// <param name="pColor">The color atith to draw the text</param>
        /// <param name="pPosX">The left mst location oe screen at witch to draw</param>
        /// <param name="pPosY">The top most location on the screen at witch to draw</param>
        public TitleText(string pText, ConsoleColor pColor, int pPosX, int pPosY)
        {
            text = pText;
            color = pColor;
            posX = pPosX;
            posY = pPosY;
        }

        /// <summary>Measures the size of the text, Note that its not in pixels but in tiles</summary>
        /// <returns>A array that contains how much tile space the tile text takes in the window (0) width (1)height</returns>
        public int[] MeasureSize()
        {
            int[] size = new int[2];
            char[] letter = new char[text.Length];
            letter = text.ToCharArray(0, text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                switch (letter[i])
                {
                    //UpperCase
                    case 'A':
                        size[0] = size[0] + 6;
                        break;
                    case 'B':
                        size[0] = size[0] + 6;
                        break;
                    case 'C':
                        size[0] = size[0] + 6;
                        break;
                    case 'D':
                        size[0] = size[0] + 6;
                        break;
                    case 'E':
                        size[0] = size[0] + 5;
                        break;
                    case 'F':
                        size[0] = size[0] + 5;
                        break;
                    case 'G':
                        size[0] = size[0] + 6;
                        break;
                    case 'H':
                        size[0] = size[0] + 6;
                        break;
                    case 'I':
                        size[0] = size[0] + 4;
                        break;
                    case 'J':
                        size[0] = size[0] + 6;
                        break;
                    case 'K':
                        size[0] = size[0] + 5;
                        break;
                    case 'L':
                        size[0] = size[0] + 5;
                        break;
                    case 'M':
                        size[0] = size[0] + 6;
                        break;
                    case 'N':
                        size[0] = size[0] + 6;
                        break;
                    case 'O':
                        size[0] = size[0] + 6;
                        break;
                    case 'P':
                        size[0] = size[0] + 6;
                        break;
                    case 'Q':
                        size[0] = size[0] + 6;
                        break;
                    case 'R':
                        size[0] = size[0] + 6;
                        break;
                    case 'S':
                        size[0] = size[0] + 6;
                        break;
                    case 'T':
                        size[0] = size[0] + 4;
                        break;
                    case 'U':
                        size[0] = size[0] + 6;
                        break;
                    case 'V':
                        size[0] = size[0] + 6;
                        break;
                    case 'W':
                        size[0] = size[0] + 6;
                        break;
                    case 'X':
                        size[0] = size[0] + 6;
                        break;
                    case 'Y':
                        size[0] = size[0] + 6;
                        break;
                    case 'Z':
                        size[0] = size[0]+ 4;
                        break;
                    //Numbers
                    case '0':
                        size[0] = size[0] + 6;
                        break;
                    case '1':
                        size[0] = size[0] + 4;
                        break;
                    case '2':
                        size[0] = size[0] + 5;
                        break;
                    case '3':
                        size[0] = size[0] + 5;
                        break;
                    case '4':
                        size[0] = size[0] + 5;
                        break;
                    case '5':
                        size[0] = size[0] + 5;
                        break;
                    case '6':
                        size[0] = size[0] + 5;
                        break;
                    case '7':
                        size[0] = size[0] + 5;
                        break;
                    case '8':
                        size[0] = size[0] + 6;
                        break;
                    case '9':
                        size[0] = size[0] + 5;
                        break;
                    //Special Chars
                    case ' ':
                        size[0] = size[0] + 3;
                        break;
                    case '\'':
                        size[0] = size[0] + 4;
                        break;
                    case ':':
                        size[0] = size[0] + 3;
                        break;
                }
                if(i == text.Length - 1)
                    size[0] = size[0] - 1;
            }
            size[1] = 5;
            return size;
        }

        /// <summary>Updates the text, Does noting</summary>
        public void Update() { }

        /// <summary>Draws the text on the screen</summary>
        public void Draw()
        {
            char[] letter = new char[text.Length];
            letter = text.ToCharArray(0, text.Length);
            Console.ForegroundColor = color;
            Console.BackgroundColor = Program.backgroundColor;
            int left = posX;
            int top = posY;
            for (int i = 0; i < text.Length; i++)
            {
                try
                {
                    switch (letter[i])
                    {
                        //UpperCase
                        case 'A':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ███ ");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'B':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 6;
                            break;
                        case 'C':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ████");
                            left = left + 6;
                            break;
                        case 'D':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 6;
                            break;
                        case 'E':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("███");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        case 'F':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("███");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█");
                            left = left + 5;
                            break;
                        case 'G':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█  ██");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ████");
                            left = left + 6;
                            break;
                        case 'H':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'I':
                            Console.SetCursorPosition(left, top);
                            Console.Write("███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("███");
                            left = left + 4;
                            break;
                        case 'J':
                            Console.SetCursorPosition(left, top);
                            Console.Write("  ███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ██");
                            left = left + 6;
                            break;
                        case 'K':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█ █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("██");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█ █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█  █");
                            left = left + 5;
                            break;
                        case 'L':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        case 'M':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("██ ██");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█ █ █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'N':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("██  █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█ █ █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█  ██");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'O':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ███ ");
                            left = left + 6;
                            break;
                        case 'P':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█");
                            left = left + 6;
                            break;
                        case 'Q':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ██");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ████");
                            left = left + 6;
                            break;
                        case 'R':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'S':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" ███");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("    █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 6;
                            break;
                        case 'T':
                            Console.SetCursorPosition(left, top);
                            Console.Write("███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" █");
                            left = left + 4;
                            break;
                        case 'U':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ███");
                            left = left + 6;
                            break;
                        case 'V':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write(" █ █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("  █");
                            left = left + 6;
                            break;
                        case 'W':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█ █ █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("██ ██");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'X':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write(" █ █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("  █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write(" █ █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("█   █");
                            left = left + 6;
                            break;
                        case 'Y':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write(" █ █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("  █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("  █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("  █");
                            left = left + 6;
                            break;
                        case 'Z':
                            Console.SetCursorPosition(left, top);
                            Console.Write("███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("  █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("███");
                            left = left + 4;
                            break;
                        //Numbers
                        case '0':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█  ██");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("█ █ █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("██  █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ███");
                            left = left + 6;
                            break;
                        case '1':
                            Console.SetCursorPosition(left, top);
                            Console.Write("██");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write(" █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("███");
                            left = left + 4;
                            break;
                        case '2':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        case '3':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" ███");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        case '4':
                            Console.SetCursorPosition(left, top);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("   █");
                            left = left + 5;
                            break;
                        case '5':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   ");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        case '6':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        case '7':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("   █");
                            left = left + 5;
                            break;
                        case '8':
                            Console.SetCursorPosition(left, top);
                            Console.Write(" ███");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write(" ███");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("█   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write(" ███");
                            left = left + 6;
                            break;
                        case '9':
                            Console.SetCursorPosition(left, top);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write("█  █");
                            Console.SetCursorPosition(left, top + 2);
                            Console.Write("████");
                            Console.SetCursorPosition(left, top + 3);
                            Console.Write("   █");
                            Console.SetCursorPosition(left, top + 4);
                            Console.Write("████");
                            left = left + 5;
                            break;
                        //Special Chars
                        case ' ':
                            left = left + 3;
                            break;
                        case '\'':
                            Console.SetCursorPosition(left, top - 1);
                            Console.Write(" ██");
                            Console.SetCursorPosition(left, top);
                            Console.Write("  █");
                            Console.SetCursorPosition(left, top + 1);
                            Console.Write(" █");
                            left = left + 4;
                            break;
                        case ':':
                            Console.SetCursorPosition(left,top+1);
                            Console.Write("██");
                            Console.SetCursorPosition(left, top+3);
                            Console.Write("██");
                            left = left + 3;
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    CrashReporter.CreateCrashReport(e, new string[] { "Text being drawn:" + text, "Left:" + left + " Top:" + top, "letter dawing:" + letter[i] });
                }
            }
            Console.ResetColor();
        }
    }
}
