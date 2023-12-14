using DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace Col.DB;

public class Context : DbContext
{
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<ExperimentConditionEntity> Experiments { get; set; }
    
    
    public Context()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename = database.db3");
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