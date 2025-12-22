using Raylib_cs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XmasGames.Data;
using XmasGames.HangSanta;
using XmasGames.Models;
using XmasGames.Xmas_Quiz;
using XmasGames.XmasSnake;

namespace XmasGames.Menu
{
    internal class MenuHelper
    {
            private XmasGamesDBContext ?context;

        public class StartMenu
        {
            public string[] Options { get; private set; }

            // Index for current option
            public int SelectedIndex { get; private set; } = 0;

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
                        var quiz = new QuizService(context);
                        quiz.StartQuiz(1);
                        break;

                    case "Hang Santa":
                        var game = new StartGame(context);
                        game.Run();
                        break;

                    case "Xmas Snake":
                        Countdown(5);
                        var snakeGame = new StartSnake();
                        snakeGame.runSnake();
                        break;

                    case "Highscore":
                        Console.WriteLine("Visar Highscore...");
                        // lägg logik här
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
            Raylib.CloseWindow();
        }
    }
}