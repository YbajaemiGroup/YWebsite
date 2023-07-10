using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YDatabase.Models;

namespace YDatabase;

public partial class YbajaemiContext : DbContext
{
    private string connectionString;

    public YbajaemiContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public YbajaemiContext(string connectionString, DbContextOptions<YbajaemiContext> options)
            : base(options)
    {
        this.connectionString = connectionString;
    }
    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.ToTable("games", "tournier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UpdationId).HasColumnName("updation_id");
            entity.Property(e => e.Row).HasColumnName("row");
            entity.Property(e => e.IsGroup).HasColumnName("is_group");
            entity.Property(e => e.IsUpper).HasColumnName("is_upper");
            entity.Property(e => e.Player1).HasColumnName("player1");
            entity.Property(e => e.Player2).HasColumnName("player2");
            entity.Property(e => e.Round).HasColumnName("round");
            entity.Property(e => e.Winner).HasColumnName("winner");

            entity.HasOne(d => d.Player1Navigation).WithMany(p => p.GamePlayer1Navigations)
                .HasForeignKey(d => d.Player1)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_game_player1");

            entity.HasOne(d => d.Player2Navigation).WithMany(p => p.GamePlayer2Navigations)
                .HasForeignKey(d => d.Player2)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_game_player2");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("images_pkey");

            entity.ToTable("images", "tournier");

            entity.HasIndex(e => e.ImageName, "images_image_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageName)
                .HasColumnType("character varying")
                .HasColumnName("image_name");
            entity.Property(e => e.IsStaff).HasColumnName("is_staff");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("links_pkey");

            entity.ToTable("links", "tournier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descr).HasColumnName("descr");
            entity.Property(e => e.Link1)
                .HasColumnType("character varying")
                .HasColumnName("link");
            entity.Property(e => e.Player).HasColumnName("player");

            entity.HasOne(d => d.PlayerNavigation).WithMany(p => p.Links)
                .HasForeignKey(d => d.Player)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_link_player");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_pkey");

            entity.ToTable("logs", "tournier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time");
            entity.Property(e => e.Exception)
                .HasColumnType("character varying")
                .HasColumnName("exception");
            entity.Property(e => e.Message)
                .HasColumnType("character varying")
                .HasColumnName("message");
            entity.Property(e => e.Severety)
                .HasMaxLength(64)
                .HasColumnName("severety");
            entity.Property(e => e.Source)
                .HasMaxLength(256)
                .HasColumnName("source");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("players_pkey");

            entity.ToTable("players", "tournier");

            entity.HasIndex(e => e.Nickname, "players_nickname_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descr).HasColumnName("descr");
            entity.Property(e => e.GroupNumber).HasColumnName("group_number");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Lose)
                .HasDefaultValueSql("0")
                .HasColumnName("lose");
            entity.Property(e => e.Nickname)
                .HasColumnType("character varying")
                .HasColumnName("nickname");
            entity.Property(e => e.Points)
                .HasDefaultValueSql("0")
                .HasColumnName("points");
            entity.Property(e => e.Won)
                .HasDefaultValueSql("0")
                .HasColumnName("won");

            entity.HasOne(d => d.Image).WithMany(p => p.Players)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_player_image");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Hash).HasName("tokens_pkey");

            entity.ToTable("tokens", "auth");

            entity.Property(e => e.Hash)
                .HasColumnType("character varying")
                .HasColumnName("hash");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
