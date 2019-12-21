using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Screens.UIElements
{
    public class TextPage
    {
        private List<MultiColoredTextLabel> textLines; //A list of MultiColoredTextLabels that make up the page

        /// <summary>Base constructor</summary>
        public TextPage()
        {
            textLines = new List<MultiColoredTextLabel>();
        }

        /// <summary>Add a new line of text in the page</summary>
        /// <param name="text">The text to show, the base object is a MultiColoredTextLabel so color code are useble</param>
        /// <param name="posX">The X location on the screen where the line is drawn</param>
        /// <param name="poY">The Y location on the screen where the line is drawn</param>
        /// <param name="baseColor"></param>
        public void AddNewLine(string text, int posX, int posY, ConsoleColor baseColor)
        {
            textLines.Add(new MultiColoredTextLabel(text, posX, posY, baseColor));
        }

        /// <summary>Dose nothing only here just incease</summary>
        public void Update() { }

        /// <summary>Draws the text lines</summary>
        public void Draw()
        {
            foreach (MultiColoredTextLabel line in textLines)
                line.Draw();
        }
    }
}
