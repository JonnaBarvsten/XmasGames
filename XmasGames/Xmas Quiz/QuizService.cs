using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using XmasGames.Data;
using XmasGames.Models;
using XmasGames.HangSanta;

namespace XmasGames.Xmas_Quiz
{
    internal class QuizService
    {
        private readonly XmasGamesDBContext _context;

        public QuizService(XmasGamesDBContext context)
        {
            _context = context;
        }

        public void StartQuiz(int miniGameId)
        {
            var minigame = _context.MiniGames.FirstOrDefault(m => m.GameName == "Xmas Quiz");
            if (minigame == null)
            {
                Console.WriteLine("Game was not found!");
                return;
            }

            int points = minigame.PointsAwarded;
            int totalScore = 0;

            // Get questions from database
            var questions = _context.QuizQuestions
                .Where(q => q.MiniGameId == minigame.MiniGameId)
                .ToList();

            var random = new Random();
            questions = questions.OrderBy(q => random.Next()).Take(3).ToList();

            Raylib.InitWindow(1500, 600, "Xmas Quiz");
            Raylib.SetTargetFPS(60);

            // Create snowflakes
            List<Snowflake> snowflakes = new List<Snowflake>();
            for (int i = 0; i < 100; i++)
                snowflakes.Add(new Snowflake(random.Next(0, 1000), random.Next(0, 600), random.Next(1, 4)));

            int currentQuestionIndex = 0;
            char answer = '\0';
            bool questionAnswered = false;

            while (!Raylib.WindowShouldClose() && currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];

                // Update snowflakes
                foreach (var s in snowflakes)
                {
                    s.Y += s.Speed;
                    if (s.Y > 600) s.Y = 0;
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(new Color(173, 216, 230, 255)); // lightblue background

                // Draw snowflakes
                foreach (var s in snowflakes)
                {
                    Raylib.DrawCircle((int)s.X, (int)s.Y, 3, Color.White);
                }

                // Draw question
                Raylib.DrawText(question.QuestionText, 50, 50, 30, Color.Black);

                // Draw choices
                Raylib.DrawText($"A - {question.OptionA}", 50, 150, 25, Color.DarkGray);
                Raylib.DrawText($"B - {question.OptionB}", 50, 200, 25, Color.DarkGray);
                Raylib.DrawText($"C - {question.OptionC}", 50, 250, 25, Color.DarkGray);
                Raylib.DrawText($"D - {question.OptionD}", 50, 300, 25, Color.DarkGray);

                // Feedback
                if (questionAnswered)
                {
                    if (answer == question.CorrectAnswer[0])
                        Raylib.DrawText("Ho ho ho correct answer!", 50, 400, 30, Color.Green);
                    else
                        Raylib.DrawText($"Santa is disappointed... Right answer: {question.CorrectAnswer}", 50, 400, 30, Color.Red);

                    Raylib.DrawText("Press Enter to continue...", 50, 500, 25, Color.DarkBlue);
                }

                Raylib.EndDrawing();

                // Handle input
                if (!questionAnswered)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.A)) { answer = 'A'; questionAnswered = true; }
                    else if (Raylib.IsKeyPressed(KeyboardKey.B)) { answer = 'B'; questionAnswered = true; }
                    else if (Raylib.IsKeyPressed(KeyboardKey.C)) { answer = 'C'; questionAnswered = true; }
                    else if (Raylib.IsKeyPressed(KeyboardKey.D)) { answer = 'D'; questionAnswered = true; }
                }
                else
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        if (answer == question.CorrectAnswer[0])
                            totalScore += points;

                        currentQuestionIndex++;
                        questionAnswered = false;
                        answer = '\0';
                    }
                }
            }


            // Final score
            bool scoreScreen = true;
            while (!Raylib.WindowShouldClose() && scoreScreen)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(new Color(173, 216, 230, 255));

                Raylib.DrawText("Quiz finished! Santa says thanks for the help!", 50, 150, 40, Color.Red);
                Raylib.DrawText($"Total Score - {totalScore}", 50, 250, 50, Color.Green);
                Raylib.DrawText("Press any key to return to main menu...", 50, 400, 30, Color.DarkBlue);

                Raylib.EndDrawing();

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    scoreScreen = false;
            }
            Raylib.CloseWindow();
        }
    }
}
// Handle life
