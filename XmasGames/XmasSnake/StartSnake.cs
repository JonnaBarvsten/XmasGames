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

            // Spelrutan
            int gameWidth = 400;
            int gameHeight = 400;
            int gridWidth = gameWidth / gridSize;
            int gridHeight = gameHeight / gridSize;

            int offsetX = 50; // spelrutans x-position
            int offsetY = 50; // spelrutans y-position

            Raylib.InitWindow(screenWidth, screenHeight, "XmasSnake");
            Raylib.SetTargetFPS(10);

            Snake snake = new Snake(gridWidth / 2, gridHeight / 2);
            Present present = new Present(gridWidth, gridHeight);
            present.Spawn(snake.Body);

            int score = 0;

            // ===== Skapa snöflingor =====
            List<Snowflake> snowflakes = new List<Snowflake>();
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                snowflakes.Add(new Snowflake(rand.Next(screenWidth), rand.Next(screenHeight), (float)(1 + rand.NextDouble() * 2)));
            }

            while (!Raylib.WindowShouldClose())
            {
                // ===== Input =====
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Up)) snake.SetDirection((0, -1));
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Down)) snake.SetDirection((0, 1));
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Left)) snake.SetDirection((-1, 0));
                if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Right)) snake.SetDirection((1, 0));

                // ===== Kolla nästa steg =====
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

                // ===== Uppdatera snöflingor =====
                foreach (var snow in snowflakes)
                {
                    snow.Y += snow.Speed;
                    if (snow.Y > screenHeight)
                    {
                        snow.Y = 0;
                        snow.X = rand.Next(screenWidth);
                    }
                }

                // ===== Rita =====
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue); // bakgrund utanför spelrutan

                // Rita snöflingor
                foreach (var snow in snowflakes)
                {
                    Raylib.DrawCircle((int)snow.X, (int)snow.Y, 2, Color.White);
                }

                // Rita spelruta
                Raylib.DrawRectangle(offsetX, offsetY, gameWidth, gameHeight, Color.DarkBlue);

                // Rita paket och orm
                present.Draw(gridSize, offsetX, offsetY);
                snake.Draw(gridSize, offsetX, offsetY);

                // Poäng
                Raylib.DrawText($"Score: {score}", 470, 60, 20, Color.Black);

                Raylib.EndDrawing();
            }

            // Game Over-skärm
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Game Over!", 200, 200, 40, Color.Red);
            Raylib.DrawText($"Final Score: {score}", 200, 260, 30, Color.White);
            Raylib.EndDrawing();

            Raylib.WaitTime(3.0f);
            Raylib.CloseWindow();


        }
    }
}
