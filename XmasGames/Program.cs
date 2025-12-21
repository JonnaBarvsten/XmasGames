using Raylib_cs;
using System;
using System.Linq;
using XmasGames;
using XmasGames.Data;
using XmasGames.Models;

namespace XmasGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new XmasGamesDBContext();

            MenuHelper.StartMenu(); 
        }
    }
}
