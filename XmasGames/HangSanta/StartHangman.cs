using Microsoft.EntityFrameworkCore;
using Raylib_cs;
using System;
using System.Collections.Generic;
using XmasGames.Data;
using XmasGames.Menu;
using XmasGames.Models;
using XmasGames.PlayerGame;
namespace XmasGames.HangSanta
{
    internal class StartGame
    {
        private const int HangSantaMiniGameId = 2;
        private readonly XmasGamesDBContext _context;
        public StartGame(XmasGamesDBContext context) 
        {
            _context = context;
        }
        public void Run()
        {
            var minigame = _context.MiniGames.FirstOrDefault(m => m.GameName == "Hang Santa");
            if (minigame == null)
            {
                Console.WriteLine("Minigame could not be found!");
                return;
            }

            int points = minigame.PointsAwarded;
            int totalScore = 0;

            Raylib.InitWindow(1000, 600, "Hangman Christmas Edition");
            Raylib.SetTargetFPS(60);

            var game = new HangMan();
            game.StartNewGame();

            // Create snowflakes
            List<Snowflake> snowflakes = new List<Snowflake>();
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
                snowflakes.Add(new Snowflake(rnd.Next(0, 800), rnd.Next(0, 600), rnd.Next(1, 4)));

            while (!Raylib.WindowShouldClose())
            {
                // Update snow
                foreach (var s in snowflakes)
                {
                    s.Y += s.Speed;
                    if (s.Y > 600) s.Y = 0;
                }

                // Handle input
                int key = Raylib.GetCharPressed();
                if (key >= 'a' && key <= 'z') game.GuessLetter((char)(key - 32));
                if (key >= 'A' && key <= 'Z') game.GuessLetter((char)key);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(new Color(173, 216, 230, 255)); 

                // Draw snow
                foreach (var s in snowflakes)
                    Raylib.DrawCircle((int)s.X, (int)s.Y, 2, Color.White);

                game.DrawHangManGraphics();

                game.DrawWord();
                game.DrawGuessedLetters();

                int textX = 450;
                int scoreY = 380;
                int messageY = 420;

                Raylib.DrawText($"Score: {totalScore}", textX, scoreY, 22, Color.Black);

                if (game.IsGameWon())
                {
                    totalScore += points;
                    Raylib.DrawText("WOHO YOU WON!", textX, messageY, 20, Color.Green);
                }
                else if (game.IsGameOver())
                {
                    Raylib.DrawText($"GAME OVER! The word was: {game.RandomWord}", textX - 50, messageY, 20, Color.Red);
                }
               
                Raylib.EndDrawing();

                if (game.IsGameWon() || game.IsGameOver())
                    break;
            }

            Raylib.WaitTime(3.0f);
            using (var context = new XmasGamesDBContext())
            {
                var gameResultService = new GameResultService(context);
                gameResultService.SaveGameResult(totalScore, HangSantaMiniGameId, "HangSanta");
            }
            return;
        }
    }
}