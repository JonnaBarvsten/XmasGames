using System;
using System.Collections.Generic;

namespace XmasGames.Models;

public partial class PlayerMiniGame
{
    public int PlayerMiniGame1 { get; set; }

    public int PlayerId { get; set; }

    public int MiniGameId { get; set; }

    public int? ScoreId { get; set; }

    public virtual MiniGame MiniGame { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
