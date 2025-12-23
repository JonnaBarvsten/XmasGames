using Raylib_cs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XmasGames.Data;
using XmasGames.HangSanta;
using XmasGames.Models;
using XmasGames.PlayerGame;
using XmasGames.Xmas_Quiz;
using XmasGames.XmasSnake;


namespace XmasGames.Menu
{
    public static class MenuHelper
    {
        private static XmasGamesDBContext context;

        private static readonly string[] Options =
        {
            "Create New Player",
            "Xmas Quiz",
            "Hang Santa",
            "Xmas Snake",
            "Highscore",
            "Exit"
        };

        private static int selectedIndex;
        private static MenuDesign? menuDesign;

        // Main method to start the menu
        public static void StartMenu(XmasGamesDBContext dbContext)
        {
            context = dbContext;
            selectedIndex = 0;
            menuDesign = new MenuDesign();

            while (!Raylib.WindowShouldClose())
            {
                HandleInput();
                Draw();
            }
        }

        // Input and logic handling
        private static void HandleInput()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Up))
                selectedIndex = (selectedIndex - 1 + Options.Length) % Options.Length;

            if (Raylib.IsKeyPressed(KeyboardKey.Down))
                selectedIndex = (selectedIndex + 1) % Options.Length;

            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                StartGame();
        }
        private static void Draw()
        {
            menuDesign?.Draw(Options, selectedIndex);
        }


        // Start game based on selected option
        private static void StartGame()
        {
            switch (Options[selectedIndex])
            {
                case "Create New Player":
                    var player = new PlayerService(context).CreatePlayer();
                    break;

                case "Xmas Quiz":
                    if (GameSession.CurrentPlayer == null)
                    {
                        Console.WriteLine("Please create a player first!");
                        Console.ReadKey();
                        break;
                    }
                    new QuizService(context).StartQuiz(1);
                    break;

                case "Hang Santa":
                    if (GameSession.CurrentPlayer == null)
                    {
                        Console.WriteLine("Please create a player first!");
                        Console.ReadKey();
                        break;
                    }
                    new StartGame(context).Run();
                    break;

                case "Xmas Snake":
                    if (GameSession.CurrentPlayer == null)
                    {
                        Console.WriteLine("Please create a player first!");
                        Console.ReadKey();
                        break;
                    }
                    Countdown(3);
                    new StartSnake().runSnake();
                    break;

                case "Highscore":
                    Console.WriteLine("Visar Highscore...");
                    break;

                case "Exit":
                    Environment.Exit(0);
                    break;
            }
        }

        //Countdown for starting games
        private static void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                double start = Raylib.GetTime();

                while (Raylib.GetTime() - start < 1)
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Raylib_cs.Color.Black);
                    Raylib.DrawText(i.ToString(), 430, 260, 80, Raylib_cs.Color.Red);
                    Raylib.EndDrawing();
                }
            }
            Raylib.CloseWindow();
        }
    }
}
