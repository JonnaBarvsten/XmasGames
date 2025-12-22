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
            int gridSize = 20;
            int gameWidth = 400;
            int gameHeight = 400;

            int gridWidth = gameWidth / gridSize;
            int gridHeight = gameHeight / gridSize;

            int offsetX = 50;
            int offsetY = 50;

            Raylib.SetTargetFPS(10);

            Snake snake = new Snake(gridWidth / 2, gridHeight / 2);
            Present present = new Present(gridWidth, gridHeight);
            present.Spawn(snake.Body);

            int score = 0;

            List<Snowflake> snowflakes = new();
            Random rand = new();

            for (int i = 0; i < 50; i++)
                snowflakes.Add(new Snowflake(rand.Next(900), rand.Next(600), (float)(1 + rand.NextDouble() * 2)));

            bool playing = true;

            while (playing)
            {
                // ESC = back to menu
                if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                    playing = false;

                // Input
                if (Raylib.IsKeyPressed(KeyboardKey.Up)) snake.SetDirection((0, -1));
                if (Raylib.IsKeyPressed(KeyboardKey.Down)) snake.SetDirection((0, 1));
                if (Raylib.IsKeyPressed(KeyboardKey.Left)) snake.SetDirection((-1, 0));
                if (Raylib.IsKeyPressed(KeyboardKey.Right)) snake.SetDirection((1, 0));

                var nextHead = (snake.Body[0].x + snake.Direction.x, snake.Body[0].y + snake.Direction.y);
                if (snake.CheckCollisionAt(nextHead, gridWidth, gridHeight))
                    playing = false;

                bool grow = nextHead == present.Position;
                snake.Move(grow);

                if (grow)
                {
                    score++;
                    present.Spawn(snake.Body);
                }

                foreach (var snow in snowflakes)
                {
                    snow.Y += snow.Speed;
                    if (snow.Y > 600)
                    {
                        snow.Y = 0;
                        snow.X = rand.Next(900);
                    }
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue);

                foreach (var snow in snowflakes)
                    Raylib.DrawCircle((int)snow.X, (int)snow.Y, 2, Color.White);

                Raylib.DrawRectangle(offsetX, offsetY, gameWidth, gameHeight, Color.DarkBlue);
                present.Draw(gridSize, offsetX, offsetY);
                snake.Draw(gridSize, offsetX, offsetY);
                Raylib.DrawText($"Score: {score}", 470, 60, 20, Color.Black);

                Raylib.EndDrawing();
            }

            // Game over screen 
            double t = Raylib.GetTime();
            while (Raylib.GetTime() - t < 2)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.DrawText("Game Over", 280, 250, 40, Color.Red);
                Raylib.EndDrawing();
            }
        }
    }
}
