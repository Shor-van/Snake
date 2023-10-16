using System;

using Snake.Entities;

namespace Snake
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Entities.Snake snake = new Entities.Snake(25,5,5,Direction.Up);
            snake.Draw();

            while(true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if(keyInfo.Key == ConsoleKey.W) snake.Move(Direction.Up);
                else if(keyInfo.Key == ConsoleKey.S) snake.Move(Direction.Down);
                else if(keyInfo.Key == ConsoleKey.A) snake.Move(Direction.Left);
                else if(keyInfo.Key == ConsoleKey.D) snake.Move(Direction.Right);

                snake.Draw();
            }
        }
    }
}
