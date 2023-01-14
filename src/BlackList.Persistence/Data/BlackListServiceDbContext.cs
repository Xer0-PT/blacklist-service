namespace BlackList.Persistence.Data;

using BlackList.Domain.Entities;
using BlackList.Persistence.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics;

public class BlackListServiceDbContext : DbContext
{
    // TODO get connection string with IConfiguration
    public BlackListServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlackListedPlayer> BlackListedPlayer { get; set; }
    public DbSet<User> User { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=blackList;UserId=postgres;Password=postgres;");
    //}

#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(x =>
        {
            Debug.WriteLine(x);
        });
    }
#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlackListedPlayerMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.Ignore<EntityBase>();
    }
}

public class BlackListServiceContextFactory : IDesignTimeDbContextFactory<BlackListServiceDbContext>
{
    public BlackListServiceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlackListServiceDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=blackList;UserId=postgres;Password=postgres;");

        return new BlackListServiceDbContext(optionsBuilder.Options);
    }
}