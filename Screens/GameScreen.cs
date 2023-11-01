using System;

using Snake.Entities;
using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary></summary>
    internal sealed class GameScreen : Screen
    {
        SnakeEntity snake = new SnakeEntity(3,5,5, ConsoleColor.Green, Direction.Up);
        FoodEntity food = new FoodEntity(7, 10);

        internal GameScreen(GameSnake gameInstance) : base(gameInstance) { }

        protected override void InitalizeScreen()
        {
            
        }

        protected override void LayoutScreen()
        {
            
        }

        internal override void HandleInput(GameTime gameTime)
        {
            
        }
        
        protected override void UpdateScreen(GameTime gameTime)
        {
            int moveAmount = 1;

            if (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.W) snake.Move(Direction.Up, moveAmount);
                else if (keyInfo.Key == ConsoleKey.S) snake.Move(Direction.Down, moveAmount);
                else if (keyInfo.Key == ConsoleKey.A) snake.Move(Direction.Left, moveAmount);
                else if (keyInfo.Key == ConsoleKey.D) snake.Move(Direction.Right, moveAmount);
            }
            
            if (food.Intersects(snake.HeadX, snake.HeadY) == true)
            {
                food = new FoodEntity(RandomHelper.RandomInt(1, 50), RandomHelper.RandomInt(1, 25));
                snake.Grow();
            }
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            snake.Draw();
            food.Draw();
        }
    }
}
