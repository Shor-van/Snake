using System;

using Snake.Entities;
using Snake.Utilities;

namespace Snake
{
    internal static class Program
    {
        private static void Main()
        {
            //Test();

            new GameSnake().Run();
        }

        private static void Test()
        {
            SnakeEntity snake = new SnakeEntity(3,5,5, ConsoleColor.Green, Direction.Up);
            FoodEntity food = new FoodEntity(7, 10);
            snake.Draw();
            food.Draw();

            while(true)
            {
                int moveAmount = 1;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if(keyInfo.Key == ConsoleKey.W) snake.Move(Direction.Up, moveAmount);
                else if(keyInfo.Key == ConsoleKey.S) snake.Move(Direction.Down, moveAmount);
                else if(keyInfo.Key == ConsoleKey.A) snake.Move(Direction.Left, moveAmount);
                else if(keyInfo.Key == ConsoleKey.D) snake.Move(Direction.Right, moveAmount);

                if (food.Intersects(snake.HeadX, snake.HeadY) == true)
                {
                    food = new FoodEntity(RandomHelper.RandomInt(1, 50), RandomHelper.RandomInt(1, 25));
                    snake.Grow();
                }

                snake.Draw();
                food.Draw();
            }
        }
    }
}
