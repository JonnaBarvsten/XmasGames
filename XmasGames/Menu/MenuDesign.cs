using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace XmasGames.Menu
{
    internal class MenuDesign
    {
        private float sledX = -300;
        private float sledY = 200;
        private float sledSpeed = 150f;

        private int reindeerCount = 3;
        private int spacing = 100;

        public void Draw(string[] options, int selectedIndex)
        {
            UpdateAnimation();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkBlue);

            DrawTitle();
            DrawSledAndReindeer();
            DrawMenu(options, selectedIndex);

            Raylib.EndDrawing();
        }

        private void UpdateAnimation()
        {
            float dt = Raylib.GetFrameTime();
            sledX += sledSpeed * dt;

            if (sledX > Raylib.GetScreenWidth() + 300)
                sledX = -300;
        }

        private void DrawTitle()
        {
            Raylib.DrawText("HO HO HO", 280, 30, 60, Color.Red);
            Raylib.DrawText("Welcome to Xmas Games", 240, 100, 30, Color.Green);
        }

        private void DrawMenu(string[] options, int selectedIndex)
        {
            for (int i = 0; i < options.Length; i++)
            {
                bool selected = i == selectedIndex;
                Color color = selected ? Color.Red : Color.RayWhite;
                string prefix = selected ? "> " : "  ";

                Raylib.DrawText(
                    prefix + options[i],
                    340,
                    350 + i * 35,
                    24,
                    color
                );
            }
        }

        private void DrawSledAndReindeer()
        {
            DrawSled(sledX, sledY);

            for (int i = 0; i < reindeerCount; i++)
            {
                int x = (int)(sledX + 150 + i * spacing);
                int y = (int)sledY;
                DrawReindeer(x, y, i * 1.5f);
            }
        }

        private void DrawSled(float x, float y)
        {
            Raylib.DrawRectangle((int)x, (int)y + 25, 120, 20, Color.Brown);
            Raylib.DrawLine((int)x, (int)y + 45, (int)x + 120, (int)y + 45, Color.Gold);

            Raylib.DrawCircle((int)x + 80, (int)y, 15, Color.Red);
            Raylib.DrawCircle((int)x + 80, (int)y - 20, 12, Color.Beige);

            Raylib.DrawTriangle(
                new Vector2(x + 68, y - 28),
                new Vector2(x + 92, y - 28),
                new Vector2(x + 80, y - 45),
                Color.Red
            );

            Raylib.DrawCircle((int)x + 80, (int)y - 45, 4, Color.White);
            Raylib.DrawLine((int)x - 10, (int)y + 45, (int)x, (int)y + 35, Color.Brown);
        }

        private void DrawReindeer(int x, int y, float phase)
        {
            float time = (float)Raylib.GetTime();
            float legSwing = MathF.Sin(time * 8f + phase) * 6f;

            Raylib.DrawEllipse(x, y, 30, 18, Color.Brown);
            Raylib.DrawCircle(x + 30, y - 10, 10, Color.Brown);
            Raylib.DrawCircle(x + 33, y - 12, 2, Color.Black);

            float glow = MathF.Sin(time * 6f + phase) * 2f;
            Raylib.DrawCircle(x + 40, y - 8, 4 + glow, Color.Red);

            DrawLeg(x - 10, y + 15, legSwing);
            DrawLeg(x + 5, y + 15, -legSwing);
            DrawLeg(x + 20, y + 15, legSwing);

            Raylib.DrawLine(x + 30, y - 20, x + 25, y - 35, Color.DarkBrown);
            Raylib.DrawLine(x + 30, y - 20, x + 35, y - 35, Color.DarkBrown);
        }

        private void DrawLeg(int x, int y, float swing)
        {
            Raylib.DrawLine(x, y, x + (int)swing, y + 15, Color.DarkBrown);
        }
    }
}
