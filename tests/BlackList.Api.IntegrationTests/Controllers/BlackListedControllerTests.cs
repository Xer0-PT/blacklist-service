namespace BlackList.Api.IntegrationTests.Controllers
{
    using BlackList.Api.IntegrationTests.Fixtures;
    using System;

    public class BlackListedControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public BlackListedControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void BlackListedPlayerController_GetAll_ReturnsAll()
        {
            await _factory.InitializeAsync();

            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/blacklistedplayer?token=teste", CancellationToken.None);

            response.EnsureSuccessStatusCode();
        }
    }
}