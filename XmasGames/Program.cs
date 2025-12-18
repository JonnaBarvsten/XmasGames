using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;
using XmasGame;
using XmasGames.HangSanta;

namespace XmasGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new StartGame();
            game.Run();
        }
    }
}