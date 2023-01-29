// using BlackList.Persistence.Data;
// using DotNet.Testcontainers.Builders;
// using DotNet.Testcontainers.Configurations;
// using DotNet.Testcontainers.Containers;
// using Microsoft.EntityFrameworkCore;
//
// namespace BlackList.Api.IntegrationTests.Fixtures;
//
// public class DatabaseFixture : IAsyncLifetime
// {
//     private PostgreSqlTestcontainer? _container;
//
//     // public string ConnectionString
//     // {
//     //     get
//     //     {
//     //         ArgumentNullException.ThrowIfNull(_container);
//     //         return _container.ConnectionString;
//     //     }
//     // }
//
//     public async Task InitializeAsync()
//     {
//         var testcontainersBuilder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
//             .WithImage("postgres:latest")
//             // .WithCleanUp(true)
//             // .WithAutoRemove(true)
//             .WithPortBinding(5432)
//             .WithBindMount(Environment.CurrentDirectory + "\\InitDb", "/docker-entrypoint-initdb.d")
//             .WithDatabase(new PostgreSqlTestcontainerConfiguration
//             {
//                 Database = "postgres",
//                 Username = "postgres",
//                 Password = "postgres",
//                 Port = 5432
//             });
//
//         _container = testcontainersBuilder.Build();
//
//         await _container.StartAsync();
//     }
//
//     public async Task DisposeAsync()
//     {
//         if (_container is not null)
//         {
//             await _container.DisposeAsync();
//         }
//     }
// }