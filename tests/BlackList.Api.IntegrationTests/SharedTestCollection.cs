using BlackList.Api.IntegrationTests.Fixtures;

namespace BlackList.Api.IntegrationTests;

[CollectionDefinition(Name)]
public class SharedTestCollection : ICollectionFixture<DatabaseFixture>, ICollectionFixture<WireMockFixture>
{
    public const string Name = "Test Collection";
}