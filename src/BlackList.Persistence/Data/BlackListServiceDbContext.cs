using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlackList.Persistence.Data;

public class BlackListServiceDbContext : DbContext
{
    public BlackListServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Player> Player { get; set; }
    public DbSet<User> User { get; set; }

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
        modelBuilder.ApplyConfiguration(new PlayerMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.Ignore<EntityBase>();
    }
}

[ExcludeFromCodeCoverage]
public class BlackListServiceContextFactory : IDesignTimeDbContextFactory<BlackListServiceDbContext>
{
    public BlackListServiceDbContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];
        var optionsBuilder = new DbContextOptionsBuilder<BlackListServiceDbContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return new BlackListServiceDbContext(optionsBuilder.Options);
    }
}