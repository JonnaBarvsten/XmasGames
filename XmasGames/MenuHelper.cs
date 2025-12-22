using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Spectre.Console;
using XmasGames.HangSanta;
using XmasGames.XmasSnake;

namespace XmasGames
{
    internal class MenuHelper
    {
        public static void StartMenu()
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("HO HO HO")
                    .Centered()
                    .Color(Color.Red));

            AnsiConsole.Write(
                new FigletText("Welcome to Xmas Games")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.MarkupLine("[green]                                                           *   [/]");
            AnsiConsole.MarkupLine("[green]                                                          ***  [/]");
            AnsiConsole.MarkupLine("[green]                                                         ***** [/]");
            AnsiConsole.MarkupLine("[green]                                                        *******[/]");
            AnsiConsole.MarkupLine("[red]                                                          |||  [/]");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Create New Player", "Xmas Quiz", "Hang Santa", "Xmas Snake", "Highscore",
                        "[Red]Exit[/]"
            }));

            switch (choice) 
            {
                case "Create New Player":
                    break;

                case "Xmas Quiz":
                    break;

                case "Hang Santa":
                    var game = new StartGame();
                    game.Run();
                    Console.Clear();
                    StartMenu();
                    break;

                case "Xmas Snake":
                    var snakeGame = new StartSnake();
                    snakeGame.runSnake();
                    Console.Clear();
                    StartMenu();
                    break;

                case "Highscore":
                    break;

                case "[Red]Exit[/]":
                    Console.Clear();
                    AnsiConsole.MarkupLine("[Red]Hope we see you NEVER again...[/]");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
