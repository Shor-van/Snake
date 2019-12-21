using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Snake.Utils;

namespace Snake.Screens.UIElements
{
    public class SplashText //Could have used the TextLabel Obj but meh
    {
        private static string[] splashTexts; //Array of the splash texts loaded from the file

        private int id; //The ID of the splash text in the array of splashTexts, is used to get the original splashText
        private int posX, posY; //The top left location of the splash text on the screen
        private string text; //The splash text to draw
        private ConsoleColor color; //The color used to draw the text

        //Gets and sets
        public int PositionX { get { return posX; } set { posX = value; } }
        public int PositionY { get { return posY; } set { posY = value; } }
        public ConsoleColor Color { get { return color; } set { color = value; } }

        /// <summary>Base constructor, Also calls LoadSplashTexts() if splashTexts is null</summary>
        /// <param name="posX">The left position on the screen at witch to draw the text</param>
        /// <param name="posY">The top position at witch to draw the text</param>
        /// <param name="color">The ConsoleColor at witch to draw the text</param>
        public SplashText(int posX, int posY, ConsoleColor color)
        {
            //Check if splash texts have been loaded
            if (splashTexts == null)
                LoadSplashTexts();

            this.color = color;
            this.posY = posY;
            this.posX = posX;

            GenerateNewSplashText();
        }

        /// <summary>Loads all the splash texts from the SplashTexts.txt file into the splashTexts array, should only be called once</summary>
        private static void LoadSplashTexts()
        {
            try
            {
                Assembly assembly;
                StreamReader reader;

                //Load file from resource
                assembly = Assembly.GetExecutingAssembly();
                reader = new StreamReader(assembly.GetManifestResourceStream("Minesweeper.Assets.SplashTexts.txt"));
                
                //Read texts
                List<string> tmpStrs = new List<string>();
                while (reader.EndOfStream == false)
                    tmpStrs.Add(reader.ReadLine());
                reader.Close();

                //Store strings in static array
                if (tmpStrs.Count > 0)
                    splashTexts = tmpStrs.ToArray<string>();
                else
                    splashTexts = new string[] { "Slpash texts failed to load/Not found." };
            }
            catch (Exception e)
            {
                Assembly assembly;
                assembly = Assembly.GetExecutingAssembly();

                string[] res = assembly.GetManifestResourceNames();
                List<string> tmp = res.ToList<string>();
                tmp.Insert(0, "Resources:");
                CrashReporter.CreateCrashReport(e, tmp.ToArray<string>());
            }
        }

        /// <summary>Checks if the spesified char is a escape char</summary>
        /// <param name="chara">The char to check</param>
        /// <returns>True if the char is a escape char else false</returns>
        private static bool IsEscapeChar(char chara)
        {
            switch (chara)
            {
                case '\a':
                    return true;
                case '\b':
                    return true;
                case '\f':
                    return true;
                case '\n':
                    return true;
                case '\r':
                    return true;
                case '\t':
                    return true;
                case '\v':
                    return true;
                case '\'':
                    return true;
                case '\"':
                    return true;
                case '\\':
                    return true;
            }
            return false;
        }

        /// <summary>Selects a new slpash text string from the splashTexts array at random</summary>
        public void GenerateNewSplashText()
        {
            int idx = RandomEx.GenerateRandom(0, splashTexts.Length);
            text = splashTexts[idx];
            this.id = idx;

            //Check if would fit in one line
            if (text.Length >= Program.ViewWidth() - posX)
            {
                int change = text.Length - (Program.ViewWidth() - posX);
                posX -= change;
            }
            else
            {
                posX = Program.ViewWidth() / 2;
            }
        }

        /// <summary>If the splash text contains '*' then they get replace with a random character else does nothing</summary>
        public void Update()
        {
            if (splashTexts[id].Contains("*"))
            {
                //Do the char swap thingy
                char[] chars = splashTexts[id].ToCharArray();
                text = "";
                for (int i = 0; i < chars.Length; i++)
                {
                    //Change
                    if (chars[i] == '*')
                    {
                        chars[i] = (char)RandomEx.GenerateRandom(0, 128);

                        //Anti space and escape chars
                        while (chars[i] == ' ' || IsEscapeChar(chars[i]))
                            chars[i] = (char)RandomEx.GenerateRandom(0, 128);
                    }
                   
                    //Rebuild
                    text = text + chars[i];
                }
            }
        }        

        /// <summary>Draws the splash text at the set location and color</summary>
        public void Draw()
        {
            try
            {
                Console.ForegroundColor = color;
                Console.BackgroundColor = Program.backgroundColor;
                Console.SetCursorPosition(posX, posY);

                Console.Write(text);

                Console.ResetColor();
            }
            catch(Exception e)
            {
                CrashReporter.CreateCrashReport(e, new string[] { "Text:" + text, "PosX:" + posX + " PosY:" + posY });
            }
        }
    }
}
