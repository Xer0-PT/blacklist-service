namespace BlackList.Persistence.Data;

using BlackList.Domain.Entities;
using BlackList.Persistence.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

public class BlackListServiceDbContext : DbContext
{
    public BlackListServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlackListedPlayer> BlackListedPlayer { get; set; }
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
        modelBuilder.ApplyConfiguration(new BlackListedPlayerMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.Ignore<EntityBase>();
    }
}

public class BlackListServiceContextFactory : IDesignTimeDbContextFactory<BlackListServiceDbContext>
{
    private readonly IConfiguration? _configuration;

    public BlackListServiceContextFactory()
    {
    }

    public BlackListServiceContextFactory(IConfiguration? configuration)
    {
        _configuration = configuration;
    }

    public BlackListServiceDbContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];
        var optionsBuilder = new DbContextOptionsBuilder<BlackListServiceDbContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return new BlackListServiceDbContext(optionsBuilder.Options);
    }
}