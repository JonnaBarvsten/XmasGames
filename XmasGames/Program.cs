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
            Raylib.InitWindow(900, 600, "Xmas Games");
            Raylib.SetTargetFPS(60);

            using var context = new XmasGamesDBContext();

            // Start the main menu 
            MenuHelper.StartMenu(context);

            Raylib.CloseWindow();
        }
    }
}
