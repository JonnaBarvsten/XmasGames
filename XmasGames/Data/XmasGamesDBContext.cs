using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using XmasGames.Models;

namespace XmasGames.Data;

public partial class XmasGamesDBContext : DbContext
{
    public XmasGamesDBContext()
    {
    }

    public XmasGamesDBContext(DbContextOptions<XmasGamesDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GameCharacter> GameCharacters { get; set; }

    public virtual DbSet<GameResult> GameResults { get; set; }

    public virtual DbSet<MiniGame> MiniGames { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerMiniGame> PlayerMiniGames { get; set; }

    public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = JONNA;Database=XmasGamesDB;Integrated Security = True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameCharacter>(entity =>
        {
            entity.HasKey(e => e.GameCharacterId).HasName("PK__GameChar__7E76C72568699472");

            entity.ToTable("GameCharacter");

            entity.Property(e => e.GameCharacterId).HasColumnName("GameCharacterID");
            entity.Property(e => e.CharacterDescription).HasMaxLength(200);
            entity.Property(e => e.CharacterName).HasMaxLength(50);
        });

        modelBuilder.Entity<GameResult>(entity =>
        {
            entity.HasKey(e => e.GameResultId).HasName("PK__GameResu__1128BBC004227070");

            entity.ToTable("GameResult");

            entity.Property(e => e.GameResultId).HasColumnName("GameResultID");
            entity.Property(e => e.Event).HasMaxLength(50);
            entity.Property(e => e.MiniGameId).HasColumnName("MiniGameID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

            entity.HasOne(d => d.MiniGame).WithMany(p => p.GameResults)
                .HasForeignKey(d => d.MiniGameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameResult_MiniGame");

            entity.HasOne(d => d.Player).WithMany(p => p.GameResults)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameResult_Player");
        });

        modelBuilder.Entity<MiniGame>(entity =>
        {
            entity.HasKey(e => e.MiniGameId).HasName("PK__MiniGame__9FE3FD83593D5955");

            entity.ToTable("MiniGame");

            entity.Property(e => e.MiniGameId).HasColumnName("MiniGameID");
            entity.Property(e => e.GameDescription).HasMaxLength(200);
            entity.Property(e => e.GameName).HasMaxLength(50);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74A8833D35B5");

            entity.ToTable("Player");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.GameCharacterId).HasColumnName("GameCharacterID");
            entity.Property(e => e.PlayerName).HasMaxLength(50);

            entity.HasOne(d => d.GameCharacter).WithMany(p => p.Players)
                .HasForeignKey(d => d.GameCharacterId)
                .HasConstraintName("FK_GameCharacter_Player");
        });

        modelBuilder.Entity<PlayerMiniGame>(entity =>
        {
            entity.HasKey(e => e.PlayerMiniGame1).HasName("PK__PlayerMi__C03EFBC1F6F62235");

            entity.ToTable("PlayerMiniGame");

            entity.Property(e => e.PlayerMiniGame1).HasColumnName("PlayerMiniGame");
            entity.Property(e => e.MiniGameId).HasColumnName("MiniGameID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.ScoreId).HasColumnName("ScoreID");

            entity.HasOne(d => d.MiniGame).WithMany(p => p.PlayerMiniGames)
                .HasForeignKey(d => d.MiniGameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerMiniGame_MiniGame");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerMiniGames)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MiniGame_Player");
        });

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasKey(e => e.QuizQuestionId).HasName("PK__QuizQues__45E34D5E63440EE6");

            entity.ToTable("QuizQuestion");

            entity.Property(e => e.QuizQuestionId).HasColumnName("QuizQuestionID");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MiniGameId).HasColumnName("MiniGameID");
            entity.Property(e => e.OptionA).HasMaxLength(200);
            entity.Property(e => e.OptionB).HasMaxLength(200);
            entity.Property(e => e.OptionC).HasMaxLength(200);
            entity.Property(e => e.OptionD).HasMaxLength(200);
            entity.Property(e => e.QuestionText).HasMaxLength(200);

            entity.HasOne(d => d.MiniGame).WithMany(p => p.QuizQuestions)
                .HasForeignKey(d => d.MiniGameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizQuestion_MiniGame");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
