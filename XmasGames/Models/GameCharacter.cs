using System;
using System.Collections.Generic;

namespace XmasGames.Models;

public partial class GameCharacter
{
    public int GameCharacterId { get; set; }

    public string CharacterName { get; set; } = null!;

    public string CharacterDescription { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
