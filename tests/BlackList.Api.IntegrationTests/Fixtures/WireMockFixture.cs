using System.Net;
using System.Text.Json;
using BlackList.Application.Dtos;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace BlackList.Api.IntegrationTests.Fixtures;

public class WireMockFixture : IAsyncLifetime
{
    private WireMockServer? _wireMockServer;

    public WireMockServer? WireMockServer => _wireMockServer;

    public void GetRequest(HttpStatusCode statusCode, string nickname, Guid? expectedFaceitId = null)
    {
        var request = Request.Create()
            .WithPath("/players")
            .UsingGet()
            .WithParam("nickname", nickname);

        var faceitApiResponse = new FaceitUserDetails { PlayerId = expectedFaceitId ?? Guid.NewGuid() };
        
        var response = Response.Create()
            .WithBody(JsonSerializer.Serialize(faceitApiResponse))
            .WithHeader("Content-Type", "application/json")
            .WithStatusCode(statusCode);
        
        _wireMockServer!
            .Given(request)
            .RespondWith(response);
    }
    
    public Task InitializeAsync()
    {
        _wireMockServer = WireMockServer.Start();
        
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        if (_wireMockServer is not null)
        {
            _wireMockServer.Dispose();
        }

        return Task.CompletedTask;
    }
}