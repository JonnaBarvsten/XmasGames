using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XmasGames.Data;
using XmasGames.Models;

namespace XmasGames.PlayerGame
{
    public class GameResultService
    {

        private readonly XmasGamesDBContext _context;

        public GameResultService(XmasGamesDBContext context)
        {
            _context = context;
        }

        // Save score after a minigame
        public void SaveGameResult(int score, int miniGameId, string? gameEvent = null)
        {
            var player = GameSession.CurrentPlayer;
            if (player == null) return;

            var result = new GameResult
            {
                PlayerId = player.PlayerId,
                MiniGameId = miniGameId,
                Score = score,
                Event = gameEvent,
                CreatedAt = DateTime.Now
            };

            _context.GameResults.Add(result);
            _context.SaveChanges();
        }

        // Highscore för ett specifikt minigame - ta bort?
        public List<GameResult> GetHighScoresForMiniGame(int miniGameId, int limit = 10)
        {
            return _context.GameResults
                .Include(gr => gr.Player)
                .Where(gr => gr.MiniGameId == miniGameId)
                .OrderByDescending(gr => gr.Score)
                .Take(limit)
                .ToList();
        }
        // Show top 10 highscores across all minigames
        public List<GameResult> GetAllHighScores(int top = 10)
        {
            return _context.GameResults
                .Include(gr => gr.Player)
                .Include(gr => gr.MiniGame)
                .OrderByDescending(gr => gr.Score)
                .Take(top)
                .ToList();
        }

    }
}
