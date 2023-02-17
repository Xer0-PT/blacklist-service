using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;

namespace BlackList.Api.IntegrationTests.Fixtures;

public sealed class DatabaseFixture : IAsyncLifetime
{
    private PostgreSqlTestcontainer? _container;
    
    public async Task InitializeAsync()
    {
        var testcontainersBuilder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithImage("postgres:latest")
            .WithCleanUp(true)
            .WithAutoRemove(true)
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "postgres",
                Username = "postgres",
                Password = "postgres"
            });

        _container = testcontainersBuilder.Build();

        await _container.StartAsync();
        await InitializeDatabase();
    }

    public async Task DisposeAsync()
    {
        if (_container is not null)
        {
            await _container.DisposeAsync();
        }
    }

    private async Task InitializeDatabase()
    {
        await using var context = CreateDbContext();

        await context.Database.MigrateAsync();
    }

    public string ConnectionString
    {
        get
        {
            ArgumentNullException.ThrowIfNull(_container);
            return _container.ConnectionString;
        }
    }

    private BlackListServiceDbContext CreateDbContext()
    {
        ArgumentNullException.ThrowIfNull(_container);
        
        return new BlackListServiceDbContext(
            new DbContextOptionsBuilder<BlackListServiceDbContext>()
                .UseNpgsql(_container.ConnectionString)
                .Options);
    }

    public async Task InsertUserAsync(User data)
    {
        await using var context = CreateDbContext();

        await context.User.AddAsync(data);
        await context.SaveChangesAsync();
    }
    
    public async Task InsertPlayerAsync(Player data)
    {
        await using var context = CreateDbContext();

        await context.Player.AddAsync(data);
        await context.SaveChangesAsync();
    }

    public async Task InsertPlayerAsync(IEnumerable<Player> data)
    {
        await using var context = CreateDbContext();

        await context.Player.AddRangeAsync(data);
        await context.SaveChangesAsync();
    }
}