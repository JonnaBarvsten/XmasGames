using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XmasGames.Data;
using XmasGames.Models;

namespace XmasGames
{
    public class PlayerService
    {
        private readonly XmasGamesDBContext _context;

        public PlayerService(XmasGamesDBContext context) 
        {
            _context = context;
        }

        public void CreatePlayer() 
        {
            Console.WriteLine("Please enter a name: ");
            string playerName= Console.ReadLine();

            if (string.IsNullOrWhiteSpace(playerName)) 
            {
                Console.WriteLine("Name cannot be empty!");
                return;
            }

            var characters = _context.GameCharacters.ToList();

            if (characters.Count == 0) 
            {
                Console.WriteLine("No character found in database!");
                return;
            }

            var random = new Random();
            var selectedCharacter = characters[random.Next(characters.Count)];

            var player = new Player 
            {
                PlayerName = playerName,
                RemainingLives = 6,
                CreatedAt = DateTime.Now,
                GameCharacterId = selectedCharacter.GameCharacterId
            };

            _context.Players.Add(player);
            _context.SaveChanges();

            Console.WriteLine($"Player - {playerName} is created!");
            Console.WriteLine($"Assigned character - {selectedCharacter.CharacterName}");
            Console.WriteLine($"{selectedCharacter.CharacterDescription}");
            Console.ReadKey();
            Console.WriteLine("Press any key to continue...");
        }
    }
}
