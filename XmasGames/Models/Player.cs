using System;
using System.Collections.Generic;

namespace XmasGames.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string PlayerName { get; set; } = null!;

    public int RemainingLives { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? GameCharacterId { get; set; }

    public virtual GameCharacter? GameCharacter { get; set; }

    public virtual ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();

    public virtual ICollection<PlayerMiniGame> PlayerMiniGames { get; set; } = new List<PlayerMiniGame>();
}
