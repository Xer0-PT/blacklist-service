namespace BlackList.Persistence.Data;

using BlackList.Domain.Entities;
using BlackList.Persistence.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class BlackListServiceDbContext : DbContext
{
    public BlackListServiceDbContext(DbContextOptions options) : base(options)
    {}

    public DbSet<BlackListedPlayer> BlackListedPlayer { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO inject configuration!!!
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=blackList;UserId=postgres;Password=postgres;");
    }

//#if DEBUG
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.LogTo(x =>
//        {
//            Debug.WriteLine(x);
//        });
//    }
//#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlackListedPlayerMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.Ignore<EntityBase>();
    }
}
