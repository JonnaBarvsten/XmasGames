using System;
using System.Collections.Generic;

namespace XmasGames.Models;

public partial class MiniGame
{
    public int MiniGameId { get; set; }

    public string GameName { get; set; } = null!;

    public string GameDescription { get; set; } = null!;

    public int MaxLives { get; set; }

    public int PointsAwarded { get; set; }

    public virtual ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();

    public virtual ICollection<PlayerMiniGame> PlayerMiniGames { get; set; } = new List<PlayerMiniGame>();

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
}
