using Microsoft.EntityFrameworkCore;
using Raylib_cs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XmasGames.Data;
using XmasGames.HangSanta;
using XmasGames.XmasSnake;

namespace XmasGames
{
    internal class MenuHelper
    {
        private XmasGamesDBContext context;

        public class StartMenu
        {
            public string[] Options { get; private set; }

            // Index for current option
            public int SelectedIndex { get; private set; } = 0;

            // Valfri databas-/spelkoppling
            private XmasGamesDBContext context;

            // constructor
            public StartMenu(XmasGamesDBContext dbContext)
            {
                context = dbContext;

                Options = new string[]
                {
                    "Create New Player",
                    "Xmas Quiz",
                    "Hang Santa",
                    "Xmas Snake",
                    "Highscore",
                    "Exit"
                };
            }

            public void MoveUp()
            {
                SelectedIndex = (SelectedIndex - 1 + Options.Length) % Options.Length;
            }

            public void MoveDown()
            {
                SelectedIndex = (SelectedIndex + 1) % Options.Length;
            }

            // get current option
            public string GetSelectedOption()
            {
                return Options[SelectedIndex];
            }

            // menu choice execution
            public void ExecuteSelected()
            {
                string choice = GetSelectedOption();

                switch (choice)
                {
                    case "Create New Player":
                        var player = new PlayerService(context);
                        player.CreatePlayer();
                        break;

                    case "Xmas Quiz":
                        Console.WriteLine("Startar Xmas Quiz...");
                        // Lägg till logik här
                        break;

                    case "Hang Santa":
                        var game = new StartGame();
                        game.Run();
                        break;

                    case "Xmas Snake":
                        Countdown(5);
                        var snakeGame = new StartSnake();
                        snakeGame.runSnake();
                        break;

                    case "Highscore":
                        Console.WriteLine("Visar Highscore...");
                        // Lägg till logik här
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        private static void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                double startTime = Raylib.GetTime();

                while (Raylib.GetTime() - startTime < 1)
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Raylib_cs.Color.Black);

                    Raylib.DrawText(
                        i.ToString(),
                        Raylib.GetScreenWidth() / 2 - 20,
                        Raylib.GetScreenHeight() / 2 - 40,
                        80,
                        Raylib_cs.Color.Red
                    );

                    Raylib.EndDrawing();
                }
            }
        }
    }
}

