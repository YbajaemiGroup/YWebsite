using Microsoft.EntityFrameworkCore;
using YDatabase.Models;

namespace YDatabase;

public partial class LogContext : DbContext
{
    private readonly string _connectionString;

    public LogContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public LogContext(string connectionString, DbContextOptions<YbajaemiContext> options)
            : base(options)
    {
        _connectionString = connectionString;
    }

    public virtual DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}