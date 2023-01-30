using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlackList.Api.IntegrationTests.Fixtures;

public class CustomWebApplicationFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly PostgreSqlTestcontainer _container;
    // private readonly WireMockFixture _wireMockFixture;

    public CustomWebApplicationFactory()
    {
        _container = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithImage("postgres:latest")
            .WithCleanUp(true)
            .WithAutoRemove(true)
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "postgres",
                Username = "postgres",
                Password = "postgres"
            })
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // builder.UseSetting("FaceitApiConfig:Url", _wireMockFixture.WireMockServer!.Url);
            
            services.RemoveDbContext<TDbContext>();

            services.AddDbContext<TDbContext>(options => { options.UseNpgsql(_container.ConnectionString); });

            services.EnsureDbCreated<TDbContext>();
        });
    }
    
    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        
        await using var context = CreateDbContext();
        await context.Database.MigrateAsync();
    }

    public new async Task DisposeAsync() => await _container.DisposeAsync();

    private BlackListServiceDbContext CreateDbContext()
    {
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

    public async Task InsertPlayerAsync(IEnumerable<Player> data)
    {
        await using var context = CreateDbContext();

        await context.Player.AddRangeAsync(data);
        await context.SaveChangesAsync();
    }
}

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<T>();
        context.Database.EnsureCreated();
    }
}