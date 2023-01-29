// using BlackList.Api.IntegrationTests.Fixtures;
// using Microsoft.AspNetCore.Mvc.Testing;
//
// namespace BlackList.Api.IntegrationTests.Controllers;
//
// [Collection(SharedTestCollection.Name)]
// public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
// {
//     private readonly CustomWebApplicationFactory _factory;
//     private readonly HttpClient _client;
//
//     public UserControllerTests(CustomWebApplicationFactory factory)
//     {
//         _factory = factory;
//         _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
//         {
//             AllowAutoRedirect = false
//         });
//     }
//
//     [Fact]
//     public async void UserController_CreateUser_RespondsCreatedUser()
//     {
//         // Arrange
//         var userId = new Guid();
//
//         // Act
//         var response = await _client.GetAsync($"/api/player?userFaceitId={userId}", CancellationToken.None);
//
//         // Assert
//         response.EnsureSuccessStatusCode();
//     }
// }