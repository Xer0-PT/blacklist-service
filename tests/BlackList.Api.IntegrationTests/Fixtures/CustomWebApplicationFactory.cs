using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

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
        builder.UseSetting("ConnectionStrings:BlackList", _databaseFixture.ConnectionString);
        builder.UseSetting("FaceitApiConfig:Url", _wireMockFixture.WireMockServer!.Url);
    }
}