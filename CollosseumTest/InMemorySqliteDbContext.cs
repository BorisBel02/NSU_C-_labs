using DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace CollosseumTest;

public class InMemorySqliteDbContext : DbContext
{
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<ExperimentConditionEntity> Experiments { get; set; }
    
    
    public InMemorySqliteDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=:memory:");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardEntity>().HasKey(c => c.CardId);

        modelBuilder.Entity<ExperimentConditionEntity>().HasKey(e => e.ExperimentId);
        modelBuilder.Entity<ExperimentConditionEntity>()
            .HasMany(e => e.Deck)
            .WithOne(c => c.ExperimentCondition);
    }
}