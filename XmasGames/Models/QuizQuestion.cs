using System;
using System.Collections.Generic;

namespace XmasGames.Models;

public partial class QuizQuestion
{
    public int QuizQuestionId { get; set; }

    public string CorrectAnswer { get; set; } = null!;

    public string QuestionText { get; set; } = null!;

    public string OptionA { get; set; } = null!;

    public string OptionB { get; set; } = null!;

    public string OptionC { get; set; } = null!;

    public string OptionD { get; set; } = null!;

    public int MiniGameId { get; set; }

    public virtual MiniGame MiniGame { get; set; } = null!;
}
