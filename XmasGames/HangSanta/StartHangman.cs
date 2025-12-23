using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            var minigame = _context.MiniGames
            .FirstOrDefault(m => m.GameName == "Hang Santa");
            if (minigame == null)
            {
                Console.WriteLine("Minigame could not be found!");
                return;
            }

            int points = minigame.PointsAwarded;
            int totalScore = 0;
            int lives = 2;
            bool finished = false; 

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
                // Input
                int key = Raylib.GetCharPressed();
                if (key >= 'a' && key <= 'z') game.GuessLetter((char)(key - 32));
                if (key >= 'A' && key <= 'Z') game.GuessLetter((char)key);

                // Update snow
                foreach (var s in snowflakes)
                {
                    s.Y += s.Speed;
                    if (s.Y > 600) s.Y = 0;
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(new Color(173, 216, 230, 255));

                // Snow
                foreach (var s in snowflakes)
                    Raylib.DrawCircle((int)s.X, (int)s.Y, 2, Color.White);

                // Hangman grafics
                game.DrawHangManGraphics();

                // Word-text and guessed letters
                game.DrawWord();
                game.DrawGuessedLetters();

                // Score
                Raylib.DrawText($"Score: {totalScore}", 450, 380, 22, Color.Black);

                // Lives
                Raylib.DrawText($"Lives: {lives}", 450, 50, 22, Color.Red);

                Raylib.EndDrawing();

                // Win
                if (game.IsGameWon())
                {
                    totalScore += points;

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.Black);
                    Raylib.DrawText("WOHO YOU WON!", 350, 250, 40, Color.Green);
                    Raylib.EndDrawing();
                    Raylib.WaitTime(2.0f);

                    game.StartNewGame();
                    continue;
                }

                // Lives and end of game
                if (game.IsGameOver())
                {
                    lives--;

                    if (lives > 0)
                    {
                        // Show feedback
                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.Black);
                        Raylib.DrawText("Life lost!", 300, 200, 40, Color.Red);
                        Raylib.DrawText($"Lives left: {lives}", 300, 260, 30, Color.White);
                        Raylib.EndDrawing();
                        Raylib.WaitTime(2.0f);

                        // Reset
                        game.StartNewGame();
                        continue;
                    }
                    else
                    {
                        finished = true;
                        break; 
                    }
                }
            }

            // Game over 
            if (finished)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.DrawText("GAME OVER!", 300, 200, 40, Color.Red);
                Raylib.DrawText($"The word was: {game.RandomWord}", 280, 260, 30, Color.White);
                Raylib.EndDrawing();
                Raylib.WaitTime(3.0f);
            }

            // Save score
            using (var context = new XmasGamesDBContext())
            {
                var gameResultService = new GameResultService(context);
                gameResultService.SaveGameResult(totalScore, HangSantaMiniGameId, "HangSanta");
            }
            return;
        }
    }
}