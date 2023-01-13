namespace BlackList.Persistence.Data;

using BlackList.Domain.Entities;
using BlackList.Persistence.Data.Mappings;
using Microsoft.EntityFrameworkCore;

public class BlackListServiceDbContext : DbContext
{
    public BlackListServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlackListedPlayer> BlackListedPlayer { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=blackList;UserId=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlackListedPlayerMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
    }
}
