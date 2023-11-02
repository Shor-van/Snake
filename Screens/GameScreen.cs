using System;

using Snake.Entities;
using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary></summary>
    internal sealed class GameScreen : Screen
    {
        SnakeEntity playerSnake;
        FoodEntity food;

        internal GameScreen(GameSnake gameInstance) : base(gameInstance) { }

        protected override void InitalizeScreen()
        {
            playerSnake = new SnakeEntity(3, 5, 5, ConsoleColor.Green, Direction.Up);
            food = new FoodEntity(7, 10);
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

            if (Keyboard.IsKeyPressed(ConsoleKey.W)) playerSnake.Move(Direction.Up, moveAmount);
            else if (Keyboard.IsKeyPressed(ConsoleKey.S)) playerSnake.Move(Direction.Down, moveAmount);
            else if (Keyboard.IsKeyPressed(ConsoleKey.A)) playerSnake.Move(Direction.Left, moveAmount);
            else if (Keyboard.IsKeyPressed(ConsoleKey.D)) playerSnake.Move(Direction.Right, moveAmount);

            if (food.Intersects(playerSnake.HeadX, playerSnake.HeadY) == true)
            {
                food = new FoodEntity(RandomHelper.RandomInt(1, 50), RandomHelper.RandomInt(1, 25));
                playerSnake.Grow();
            }
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            playerSnake.Draw();
            food.Draw();
        }
    }
}
