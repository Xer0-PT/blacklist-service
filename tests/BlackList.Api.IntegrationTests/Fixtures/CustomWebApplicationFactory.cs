using BlackList.Persistence.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlackList.Api.IntegrationTests.Fixtures;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly WireMockFixture _wireMockFixture;
    private readonly DatabaseFixture _databaseFixture;
    public CustomWebApplicationFactory(WireMockFixture wireMockFixture, DatabaseFixture databaseFixture)
    {
        _wireMockFixture = wireMockFixture;
        _databaseFixture = databaseFixture;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("FaceitApiConfig:Url", _wireMockFixture.WireMockServer!.Url);
        builder.UseEnvironment("Development");
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<BlackListServiceDbContext>();
            
            services.AddDbContext<BlackListServiceDbContext>(options => { options.UseNpgsql(_databaseFixture.ConnectionString); });
            
            services.EnsureDbCreated<BlackListServiceDbContext>();
        });
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