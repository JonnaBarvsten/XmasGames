using Raylib_cs;
using System;
using System.Linq;
using XmasGames.Data;
using XmasGames.HangSanta;
using XmasGames.Menu;
using XmasGames.Models;
using XmasGames.XmasSnake;

namespace XmasGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Init Raylib
            Raylib.InitWindow(900, 600, "Xmas Games");
            Raylib.SetTargetFPS(60);

            // Create logic + design
            using var context = new XmasGamesDBContext();
            var menu = new MenuHelper.StartMenu(context);
            var menuDesign = new MenuDesign();

            // Mainloop
            while (!Raylib.WindowShouldClose())
            {
                // INPUT → LOGIC
                if (Raylib.IsKeyPressed(KeyboardKey.Up))
                    menu.MoveUp();

                if (Raylib.IsKeyPressed(KeyboardKey.Down))
                    menu.MoveDown();

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    menu.ExecuteSelected();

                // RENDER → DESIGN
                menuDesign.Draw(menu);

            }

            Raylib.CloseWindow();
        }
    }
}
