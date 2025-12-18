using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace XmasGames.HangSanta
{
    internal class HangMan
    {
        public string RandomWord { get; set; } = "HORSE";
        public List<char> HiddenLetters { get; set; } = new List<char>();
        public HashSet<char> GuessedLetters { get; set; } = new HashSet<char>();
        public int AttemptsLeft { get; set; }

        private string GetRandomWord()
        {
            string path = Path.Combine(AppContext.BaseDirectory,"HangSanta", "TextFile1.txt");
            string[] words = File.ReadAllLines(path);
            Random generator = new Random();
            return words[generator.Next(words.Length)].ToUpper();
        }

        public void StartNewGame()
        {
            RandomWord = GetRandomWord();
            GuessedLetters = new HashSet<char>();
            AttemptsLeft = 6;
            HiddenLetters = new List<char>();
            for (int i = 0; i < RandomWord.Length; i++)
                HiddenLetters.Add('_');
        }

        public void GuessLetter(char letter)
        {
            letter = char.ToUpper(letter);
            if (GuessedLetters.Contains(letter)) return;
            GuessedLetters.Add(letter);

            bool found = false;
            for (int i = 0; i < RandomWord.Length; i++)
            {
                if (RandomWord[i] == letter)
                {
                    HiddenLetters[i] = letter;
                    found = true;
                }
            }

            if (!found) AttemptsLeft--;
        }

        public void DisplayWord() => DrawWord();

        public bool IsGameWon() => !HiddenLetters.Contains('_');
        public bool IsGameOver() => AttemptsLeft <= 0;

        public void DrawWord()
        {
            string display = string.Join(" ", HiddenLetters);
            Raylib.DrawText(display, 450, 150, 40, Color.Black);
        }

        public void DrawGuessedLetters()
        {
            string guessed = "Guessed: " + string.Join(" ", GuessedLetters);
            Raylib.DrawText(guessed, 450, 220, 20, Color.DarkGray);
        }

        public void DrawHangManGraphics()
        {
            int startX = 100;
            int startY = 100;

            // Stolpe, överliggare och rep
            Raylib.DrawLine(startX + 100, startY + 300, startX + 100, startY, Color.Black); // stolpe
            Raylib.DrawLine(startX + 100, startY, startX + 250, startY, Color.Black);       // överliggare
            Raylib.DrawLine(startX + 250, startY, startX + 250, startY + 50, Color.Black);  // rep
            Raylib.DrawLine(startX, startY + 300, startX + 200, startY + 300, Color.Black); // baslinje

            int wrongGuess = 6 - AttemptsLeft;

            // Huvud
            if (wrongGuess > 0) Raylib.DrawCircleLines(startX + 250, startY + 80, 30, Color.Black);

            // Julmössa
            if (wrongGuess > 0)
            {
                Raylib.DrawTriangle(
                    new Vector2(startX + 220, startY + 60),
                    new Vector2(startX + 280, startY + 60),
                    new Vector2(startX + 250, startY + 30),
                    Color.Red
                );
                Raylib.DrawCircle(startX + 250, startY + 30, 5, Color.White); // tofs
            }

            // Kropp och armar/ben
            if (wrongGuess > 1) Raylib.DrawLine(startX + 250, startY + 110, startX + 250, startY + 200, Color.Black); // kropp
            if (wrongGuess > 2) Raylib.DrawLine(startX + 250, startY + 120, startX + 220, startY + 160, Color.Black); // vänster arm
            if (wrongGuess > 3) Raylib.DrawLine(startX + 250, startY + 120, startX + 280, startY + 160, Color.Black); // höger arm
            if (wrongGuess > 4) Raylib.DrawLine(startX + 250, startY + 200, startX + 220, startY + 250, Color.Black); // vänster ben
            if (wrongGuess > 5) Raylib.DrawLine(startX + 250, startY + 200, startX + 280, startY + 250, Color.Black); // höger ben
        }
    }
}


