using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Utils
{
    /// <summary>CrashReporter class</summary>
    public static class CrashReporter
    {
        /// <summary>Creates a new crash report </summary>
        /// <param name="e">The excaption that caused the crash</param>
        /// <param name="data">Any extra data about the crash</param>
        public static void CreateCrashReport(Exception e, string[] data)
        {
            Console.SetBufferSize(220, 40);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            
            //Screen start
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Holy Cr*p on a stick, somethings wrong!");
            Console.WriteLine(Console.Title + " Has crashed, GameState:" + Program.gameState);
            Console.WriteLine();
            
            //Excaption and stacktrace
            Console.WriteLine("Exception:" + e.Message);
            Console.WriteLine();
            Console.WriteLine("StackTrace");
            Console.WriteLine("=====================");
            Console.WriteLine(e.StackTrace);
            Console.WriteLine();
            
            //Extra Notes
            Console.WriteLine("Extra Notes");
            Console.WriteLine("=====================");
            for (int i = 0; i < data.Length; i++)
                Console.WriteLine(data[i]);
            
            Console.WriteLine();
            
            Console.WriteLine("*End of report*");
            Console.WriteLine("Press any key to close.");
            
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
