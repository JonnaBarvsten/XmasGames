using System;
using System.Collections.Generic;

namespace XmasGames.Models;

public partial class GameResult
{
    public int GameResultId { get; set; }

    public int Score { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Event { get; set; }

    public int PlayerId { get; set; }

    public int MiniGameId { get; set; }

    public virtual MiniGame MiniGame { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
