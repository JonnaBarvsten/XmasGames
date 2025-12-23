using Microsoft.EntityFrameworkCore;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;
using XmasGames.Data;
using XmasGames.HangSanta;
using XmasGames.PlayerGame;

namespace XmasGames.XmasSnake
{
    internal class StartSnake
    {
        private const int SnakeMiniGameId = 3;
        private readonly XmasGamesDBContext _context;

        public StartSnake(XmasGamesDBContext context)
        {
            _context = context;
        }

        public void runSnake()
        {

            int screenWidth = 800;
            int screenHeight = 600;
            int gridSize = 20;

            int gameWidth = 400;
            int gameHeight = 400;
            int gridWidth = gameWidth / gridSize;
            int gridHeight = gameHeight / gridSize;

            int offsetX = 50;
            int offsetY = 50;

            Raylib.InitWindow(screenWidth, screenHeight, "XmasSnake");
            Raylib.SetTargetFPS(10);

            Snake snake = new Snake(gridWidth / 2, gridHeight / 2);
            Present present = new Present(gridWidth, gridHeight);
            present.Spawn(snake.Body);

            var minigameInfo = _context.MiniGames
            .FirstOrDefault(m => m.MiniGameId == 3);

            int pointsPerPresent = minigameInfo != null ? minigameInfo.PointsAwarded : 1;

            int score = 0;
            int lives = 3;

            List<Snowflake> snowflakes = new List<Snowflake>();
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
                snowflakes.Add(new Snowflake(rand.Next(screenWidth), rand.Next(screenHeight), (float)(1 + rand.NextDouble() * 2)));

            bool justReset = false; // No collision right after losing a life

            while (!Raylib.WindowShouldClose() && lives > 0)
            {
                // Input and direction handling
                if (Raylib.IsKeyPressed(KeyboardKey.Up) && snake.Direction.y != 1) snake.SetDirection((0, -1));
                if (Raylib.IsKeyPressed(KeyboardKey.Down) && snake.Direction.y != -1) snake.SetDirection((0, 1));
                if (Raylib.IsKeyPressed(KeyboardKey.Left) && snake.Direction.x != 1) snake.SetDirection((-1, 0));
                if (Raylib.IsKeyPressed(KeyboardKey.Right) && snake.Direction.x != -1) snake.SetDirection((1, 0));

                // Continue only if we have a direction
                if (snake.Direction.x == 0 && snake.Direction.y == 0)
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.SkyBlue);
                    Raylib.DrawText("Press arrow key to start", 150, 250, 20, Color.Black);
                    Raylib.EndDrawing();
                    continue;
                }


                var nextHead = (snake.Body[0].x + snake.Direction.x, snake.Body[0].y + snake.Direction.y);

                bool grow = nextHead == present.Position;

                // Move snake
                snake.Move(grow);

                // Check wall-collision
                bool hitWall = snake.Body[0].x < 0 || snake.Body[0].x >= gridWidth ||
                               snake.Body[0].y < 0 || snake.Body[0].y >= gridHeight;

                // Check self-collision
                bool hitSelf = snake.Body.Skip(1).Any(p => p.x == snake.Body[0].x && p.y == snake.Body[0].y);

                if ((hitWall || hitSelf) && !justReset)
                {
                    lives--;

                    if (lives <= 0)
                        break; // Game Over!

                    // Feedback "Life lost"
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.Black);
                    Raylib.DrawText("Life lost!", 300, 200, 40, Color.Red);
                    Raylib.EndDrawing();
                    Raylib.WaitTime(1.0f);

                    // Reset snake
                    snake.ResetPosition(gridWidth / 2, gridHeight / 2);
                    present.Spawn(snake.Body); // New present position
                    justReset = true;
                    continue;
                }
                else
                {
                    justReset = false;
                }

                if (grow)
                {
                    score += pointsPerPresent;
                    present.Spawn(snake.Body);
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue);

                foreach (var s in snowflakes)
                {
                    s.Y += s.Speed;
                    if (s.Y > screenHeight)
                    {
                        s.Y = 0;
                        s.X = rand.Next(screenWidth);
                    }
                    Raylib.DrawCircle((int)s.X, (int)s.Y, 2, Color.White);
                }

                Raylib.DrawRectangle(offsetX, offsetY, gameWidth, gameHeight, Color.DarkBlue);

                present.Draw(gridSize, offsetX, offsetY);
                snake.Draw(gridSize, offsetX, offsetY);

                // Score
                Raylib.DrawText($"Score: {score}", 470, 50, 20, Color.Black);

                Raylib.DrawText("Lives:", 470, 100, 20, Color.Black);

                // Lives
                for (int i = 0; i < lives; i++)
                {
                    int circleX = 470 + i * 30;
                    int circleY = 130;
                    Raylib.DrawCircle(circleX, circleY, 10, Color.Red);
                }


                Raylib.EndDrawing();
            }

            // Game Over
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Game Over!", 200, 200, 40, Color.Red);
            Raylib.DrawText($"Final Score: {score}", 200, 260, 30, Color.White);
            Raylib.EndDrawing();
            Raylib.WaitTime(3.0f);

            // Save score
            using (var context = new XmasGamesDBContext())
            {
                var gameResultService = new GameResultService(context);
                gameResultService.SaveGameResult(score, 3, "XmasSnake");
            }

            return;
        }



        //int screenWidth = 800;
        //int screenHeight = 600;
        //int gridSize = 20;

        //// Game window
        //int gameWidth = 400;
        //int gameHeight = 400;
        //int gridWidth = gameWidth / gridSize;
        //int gridHeight = gameHeight / gridSize;

        //int offsetX = 50; // Game window x-position 
        //int offsetY = 50; // Game window y-position

        //Raylib.InitWindow(screenWidth, screenHeight, "XmasSnake");
        //Raylib.SetTargetFPS(10);

        //Snake snake = new Snake(gridWidth / 2, gridHeight / 2);
        //Present present = new Present(gridWidth, gridHeight);
        //present.Spawn(snake.Body);

        //int score = 0;
        //int lives = 3;

        //// Create snowflakes
        //List<Snowflake> snowflakes = new List<Snowflake>();
        //Random rand = new Random();
        //for (int i = 0; i < 50; i++)
        //{
        //    snowflakes.Add(new Snowflake(rand.Next(screenWidth), rand.Next(screenHeight), (float)(1 + rand.NextDouble() * 2)));
        //}

        //while (!Raylib.WindowShouldClose() && lives > 0)
        //{
        //    // Input
        //    if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Up)) snake.SetDirection((0, -1));
        //    if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Down)) snake.SetDirection((0, 1));
        //    if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Left)) snake.SetDirection((-1, 0));
        //    if (Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Right)) snake.SetDirection((1, 0));

        //    // Next move
        //    //var nextHead = (snake.Body[0].x + snake.Direction.x, snake.Body[0].y + snake.Direction.y);
        //    //if (snake.CheckCollisionAt(nextHead, gridWidth, gridHeight))
        //    //    break; // Game Over

        //    // Lägg flagga innan loopen:
        //    bool justLostLife = false;

        //    // Inne i while-loopen, direkt efter nextHead:
        //    var nextHead = (snake.Body[0].x + snake.Direction.x, snake.Body[0].y + snake.Direction.y);

        //    if (!justLostLife && snake.CheckCollisionAt(nextHead, gridWidth, gridHeight))
        //    {
        //        lives--; // Minska ett liv

        //        if (lives <= 0)
        //            break; // Alla liv slut → Game Over

        //        // Visa feedback
        //        Raylib.BeginDrawing();
        //        Raylib.ClearBackground(Color.Black);
        //        Raylib.DrawText("Life lost!", 300, 200, 40, Color.Red);
        //        Raylib.EndDrawing();
        //        Raylib.WaitTime(1.0f);

        //        // Reset snake till startposition
        //        snake.ResetPosition(gridWidth / 2, gridHeight / 2);

        //        // Sätt flagga så att vi inte kolliderar direkt nästa frame
        //        justLostLife = true;

        //        continue; // hoppa över resten av loopen denna frame
        //    }
        //    else
        //    {
        //        // Clear flaggan nästa frame
        //        justLostLife = false;
        //    }


        //    if (snake.CheckCollisionAt(nextHead, gridWidth, gridHeight))
        //    {
        //        lives--;
        //        if (lives > 0)
        //        {
        //            // Visa lite feedback innan reset
        //            Raylib.BeginDrawing();
        //            Raylib.ClearBackground(Color.Black);
        //            Raylib.DrawText("Life lost!", 300, 200, 40, Color.Red);
        //            Raylib.EndDrawing();
        //            Raylib.WaitTime(1.0f); // Vänta 1 sekund

        //            snake.ResetPosition(gridWidth / 2, gridHeight / 2);
        //            continue; // fortsätt loopen
        //        }
        //        else
        //        {
        //            // Alla liv slut, bryt loopen
        //            break;
        //        }
        //    }




        //    bool grow = nextHead == present.Position;
        //    snake.Move(grow);

        //    if (grow)
        //    {
        //        score++;
        //        present.Spawn(snake.Body);
        //    }

        //    // Update snowflakes
        //    foreach (var snow in snowflakes)
        //    {
        //        snow.Y += snow.Speed;
        //        if (snow.Y > screenHeight)
        //        {
        //            snow.Y = 0;
        //            snow.X = rand.Next(screenWidth);
        //        }
        //    }

        //    // Draw
        //    Raylib.BeginDrawing();
        //    Raylib.ClearBackground(Color.SkyBlue); // background

        //    // Draw snowflakes
        //    foreach (var snow in snowflakes)
        //    {
        //        Raylib.DrawCircle((int)snow.X, (int)snow.Y, 2, Color.White);
        //    }

        //    // Draw game window
        //    Raylib.DrawRectangle(offsetX, offsetY, gameWidth, gameHeight, Color.DarkBlue);

        //    // Draw snake and pakage
        //    present.Draw(gridSize, offsetX, offsetY);
        //    snake.Draw(gridSize, offsetX, offsetY);

        //    // Score
        //    Raylib.DrawText($"Score: {score}", 470, 60, 20, Color.Black);

        //    for (int i = 0; i < lives; i++)
        //        Raylib.DrawCircle(470 + i * 30, 100, 10, Color.Red);

        //    Raylib.EndDrawing();
        //}

        //// Game Over-window
        //Raylib.BeginDrawing();
        //Raylib.ClearBackground(Color.Black);
        //Raylib.DrawText("Game Over!", 200, 200, 40, Color.Red);
        //Raylib.DrawText($"Final Score: {score}", 200, 260, 30, Color.White);
        //Raylib.EndDrawing();
        //Raylib.WaitTime(3.0f);

        //// Save snake-score to database
        //using (var context = new XmasGamesDBContext())
        //{
        //    var gameResultService = new GameResultService(context);
        //    gameResultService.SaveGameResult(score, SnakeMiniGameId, "Xmas Snake");
        //}

        //return;
    }
}




