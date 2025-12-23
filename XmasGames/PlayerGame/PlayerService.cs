using Raylib_cs;
using System;
using System.Linq;
using XmasGames.Data;
using XmasGames.Models;
using XmasGames.PlayerGame;

namespace XmasGames.PlayerGame
{
    public class PlayerService
    {
        private readonly XmasGamesDBContext _context;

        public PlayerService(XmasGamesDBContext context)
        {
            _context = context;
        }

        public Player CreatePlayer()
        {
            string playerName = "";
            bool nameEntered = false;

            while (!nameEntered && !Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGray);

                Raylib.DrawText("Enter your player name:", 100, 100, 30, Color.White);
                Raylib.DrawText(playerName + "_", 100, 150, 30, Color.Yellow);
                Raylib.DrawText("Press ENTER to confirm", 100, 200, 20, Color.LightGray);

                Raylib.EndDrawing();

                int key = Raylib.GetCharPressed();
                if (key > 0)
                {
                    char c = (char)key;
                    if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                        playerName += c;
                }

                if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && playerName.Length > 0)
                {
                    playerName = playerName[..^1];
                }

                // Lade till denna if-sats
                if (Raylib.IsKeyPressed(KeyboardKey.Enter) && playerName.Length > 0)
                {
                    nameEntered = true;
                }
            }

            if (Raylib.WindowShouldClose())
                return null;

            // Get random character from database.
            var characters = _context.GameCharacters.ToList();
            if (characters.Count == 0)
            {
                // If no character found
                while (!Raylib.WindowShouldClose())
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.Black);
                    Raylib.DrawText("No characters found in database!", 100, 100, 30, Color.Red);
                    Raylib.EndDrawing();
                }
                return null;
            }

            var random = new Random();
            var selectedCharacter = characters[random.Next(characters.Count)];

            // Create player
            var player = new Player
            {
                PlayerName = playerName,
                RemainingLives = 6,
                CreatedAt = DateTime.Now,
                GameCharacterId = selectedCharacter.GameCharacterId
            };

            _context.Players.Add(player);
            _context.SaveChanges();

            // Save player in GameSession
            GameSession.CurrentPlayer = player;

            float timer = 0f;
            while (timer < 10f && !Raylib.WindowShouldClose())
            {
                timer += Raylib.GetFrameTime();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGreen);
                Raylib.DrawText($"Player '{playerName}' created!", 100, 100, 30, Color.White);
                Raylib.DrawText($"Assigned character: {selectedCharacter.CharacterName}", 100, 150, 25, Color.Yellow);
                Raylib.DrawText(selectedCharacter.CharacterDescription, 100, 200, 20, Color.LightGray);
                Raylib.EndDrawing();
            }

            return player;
        }
    }
}