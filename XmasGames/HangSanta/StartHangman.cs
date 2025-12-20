using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;
using XmasGames.HangSanta;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XmasGames.HangSanta
{
    internal class StartGame
    {
        public void Run()
        {
            Raylib.InitWindow(1000, 600, "Hangman Christmas Edition");
            Raylib.SetTargetFPS(60);

            var game = new HangMan();
            game.StartNewGame();

            // Skapa snöflingor
            List<Snowflake> snowflakes = new List<Snowflake>();
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
                snowflakes.Add(new Snowflake(rnd.Next(0, 800), rnd.Next(0, 600), rnd.Next(1, 4)));

            while (!Raylib.WindowShouldClose())
            {
                // Uppdatera snö
                foreach (var s in snowflakes)
                {
                    s.Y += s.Speed;
                    if (s.Y > 600) s.Y = 0;
                }

                // Hantera tangenttryckningar
                int key = Raylib.GetCharPressed();
                if (key >= 'a' && key <= 'z') game.GuessLetter((char)(key - 32));
                if (key >= 'A' && key <= 'Z') game.GuessLetter((char)key);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(new Color(173, 216, 230, 255)); // Ljusblå bakgrund

                // Rita snö
                foreach (var s in snowflakes)
                    Raylib.DrawCircle((int)s.X, (int)s.Y, 2, Color.White);

                // Rita hängmannen med julmössa
                game.DrawHangManGraphics();

                // Rita ord och gissade bokstäver höger sida
                game.DrawWord();
                game.DrawGuessedLetters();

                // Vinst / förlust
                if (game.IsGameWon())
                    Raylib.DrawText("WOHO YOU WON!", 450, 400, 20, Color.Green);
                else if (game.IsGameOver())
                    Raylib.DrawText($"GAME OVER! The word was: {game.RandomWord}", 400, 400, 20, Color.Red);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}