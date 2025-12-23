using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;
using XmasGames.Menu;
using XmasGames.PlayerGame;
using XmasGames.Data;

namespace XmasGames.PlayerGame
{
    public class HighScore
    {
        public static void ShowTopHighscores(GameResultService service, int top = 10)
        {
            // Get top scoeres from all minigames
            var highscores = service.GetAllHighScores(top);

            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                    break;

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                // Title and instructions
                Raylib.DrawText("ALL-TIME HIGHSCORES", 150, 30, 40, Color.Gold);
                Raylib.DrawText("ESC = Back", 10, 10, 20, Color.Gray);

                // Column headers
                Raylib.DrawText("Player", 50, 100, 25, Color.LightGray);
                Raylib.DrawText("Game", 250, 100, 25, Color.LightGray);
                Raylib.DrawText("Score", 500, 100, 25, Color.LightGray);

                int y = 140;
                int rank = 1;

                foreach (var hs in highscores)
                {
                    Raylib.DrawText(hs.Player.PlayerName, 50, y, 25, Color.White);
                    Raylib.DrawText(hs.MiniGame.GameName, 250, y, 25, Color.White);
                    Raylib.DrawText(hs.Score.ToString(), 500, y, 25, Color.White);
                    y += 35;
                    rank++;
                }

                if (!highscores.Any())
                    Raylib.DrawText("No scores yet!", 200, 200, 25, Color.Red);

                float deltaTime = Raylib.GetFrameTime();
                DrawChristmasTree(700, 150, 100, deltaTime);
                Raylib.EndDrawing();
            }
        }

        // Lights positions
        private static readonly (int x, int y)[] treeLights = new (int, int)[]
        {
            (-20, 20),
            (10, 40),
            (-10, 60),
            (15, 80),
            (0, 100)
        };

        // Lights state
        private static bool lightsOn = true;

        private static void DrawChristmasTree(int x, int y, int size, float deltaTime)
        {
            // Xmas tree 
            for (int i = 0; i < 5; i++)
            {
                int width = size - i * 20;
                int height = 30;
                Raylib.DrawTriangle(
                    new System.Numerics.Vector2(x, y + i * 25),
                    new System.Numerics.Vector2(x - width / 2, y + i * 25 + height),
                    new System.Numerics.Vector2(x + width / 2, y + i * 25 + height),
                    Color.DarkGreen);
            }

            // Root
            Raylib.DrawRectangle(x - 10, y + 5 * 25, 20, 30, Color.Brown);

            // Star
            Raylib.DrawCircle(x, y - 10, 10, Color.Gold);


            if (lightsOn)
            {
                // Color on lights
                Color[] colors = new Color[] { Color.Red, Color.Yellow, Color.DarkBlue, Color.DarkPurple, Color.Orange };

                for (int i = 0; i < treeLights.Length; i++)
                {
                    int lx = x + treeLights[i].x;
                    int ly = y + treeLights[i].y;

                    // Glow-effect
                    float glow = MathF.Sin((float)Raylib.GetTime() * 6f + i) * 2f;
                    Raylib.DrawCircle(lx, ly, 6 + glow, colors[i % colors.Length]);
                }
            }
        }
    }
}


        