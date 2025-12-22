using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XmasGames.Data;
using XmasGames.HangSanta;

namespace XmasGames
{
    internal class MenuHelper
    {
        public static void StartMenu()
        {
            using var context = new XmasGamesDBContext();
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
                    var player = new PlayerService(context);
                    player.CreatePlayer();
                    Console.Clear();
                    StartMenu(); 
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
