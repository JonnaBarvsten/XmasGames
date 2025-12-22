using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;
using XmasGames.HangSanta;

namespace XmasGames.XmasSnake
{
    internal class StartSnake
    {
        public void runSnake()
        {
            int screenWidth = 800;
            int screenHeight = 600;
            int gridSize = 20;

            // Game window
            int gameWidth = 400;
            int gameHeight = 400;
            int gridWidth = gameWidth / gridSize;
            int gridHeight = gameHeight / gridSize;

            int offsetX = 50; // Game window x-position 
            int offsetY = 50; // Game window y-position

            Raylib.InitWindow(screenWidth, screenHeight, "XmasSnake");
            Raylib.SetTargetFPS(10);

            Snake snake = new Snake(gridWidth / 2, gridHeight / 2);
            Present present = new Present(gridWidth, gridHeight);
            present.Spawn(snake.Body);

            int score = 0;

            // Create snowflakes
            List<Snowflake> snowflakes = new List<Snowflake>();
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                snowflakes.Add(new Snowflake(rand.Next(screenWidth), rand.Next(screenHeight), (float)(1 + rand.NextDouble() * 2)));
            }

            while (!Raylib.WindowShouldClose())
            {
                // Input
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Up)) snake.SetDirection((0, -1));
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Down)) snake.SetDirection((0, 1));
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Left)) snake.SetDirection((-1, 0));
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Right)) snake.SetDirection((1, 0));

                // Next move
                var nextHead = (snake.Body[0].x + snake.Direction.x, snake.Body[0].y + snake.Direction.y);
                if (snake.CheckCollisionAt(nextHead, gridWidth, gridHeight))
                    break; // Game Over

                bool grow = nextHead == present.Position;
                snake.Move(grow);

                if (grow)
                {
                    score++;
                    present.Spawn(snake.Body);
                }

                // Update snowflakes
                foreach (var snow in snowflakes)
                {
                    snow.Y += snow.Speed;
                    if (snow.Y > screenHeight)
                    {
                        snow.Y = 0;
                        snow.X = rand.Next(screenWidth);
                    }
                }

                // Draw
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue); // background

                // Draw snowflakes
                foreach (var snow in snowflakes)
                {
                    Raylib.DrawCircle((int)snow.X, (int)snow.Y, 2, Color.White);
                }

                // Draw game window
                Raylib.DrawRectangle(offsetX, offsetY, gameWidth, gameHeight, Color.DarkBlue);

                // Draw snake and pakage
                present.Draw(gridSize, offsetX, offsetY);
                snake.Draw(gridSize, offsetX, offsetY);

                // Score
                Raylib.DrawText($"Score: {score}", 470, 60, 20, Color.Black);

                Raylib.EndDrawing();
            }

            // Game Over-window
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Game Over!", 200, 200, 40, Color.Red);
            Raylib.DrawText($"Final Score: {score}", 200, 260, 30, Color.White);
            Raylib.EndDrawing();
            Raylib.WaitTime(3.0f);
            return;
        }
    }
}
