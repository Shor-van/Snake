using System;

using Snake.Entities;

namespace Snake
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Entities.Snake snake = new Entities.Snake(3,5,5,Direction.Up);
            Food food = new Food(7, 10);
            snake.Draw();
            food.Draw();

            while(true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if(keyInfo.Key == ConsoleKey.W) snake.Move(Direction.Up);
                else if(keyInfo.Key == ConsoleKey.S) snake.Move(Direction.Down);
                else if(keyInfo.Key == ConsoleKey.A) snake.Move(Direction.Left);
                else if(keyInfo.Key == ConsoleKey.D) snake.Move(Direction.Right);

                if (food.Intersects(snake.HeadX, snake.HeadY) == true)
                {
                    food = new Food(new Random().Next(1, 50), new Random().Next(1, 25));
                    snake.Grow();
                }

                snake.Draw();
                food.Draw();
            }
        }
    }
}
