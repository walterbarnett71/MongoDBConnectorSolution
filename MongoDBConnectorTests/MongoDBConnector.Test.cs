using System.Threading.Tasks;
using MongoDBConnectorLib;
using Xunit;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Configurations;

namespace MongoDBConnectorTests
{
    public class MongoDBConnectorTests : IAsyncLifetime
    {
        private readonly MongoDbTestcontainer _mongoContainer;

        public MongoDBConnectorTests()
        {
            _mongoContainer = new TestcontainersBuilder<MongoDbTestcontainer>()
                .WithDatabase(new MongoDbTestcontainerConfiguration
                {
                    Database = "admin",
                    Username = "root",
                    Password = "example"
                })
                .Build();
        }

        public async Task InitializeAsync() => await _mongoContainer.StartAsync();

        public async Task DisposeAsync() => await _mongoContainer.StopAsync();

        [Fact]
        public void Ping_Succeeds_When_Mongo_Is_Up()
        {
            var connector = new MongoDBConnector(_mongoContainer.ConnectionString);
            Assert.True(connector.Ping());
        }

        [Fact]
        public void Ping_Fails_With_Invalid_Connection()
        {
            var connector = new MongoDBConnector("mongodb://invalid:27017");
            Assert.False(connector.Ping());
        }
    }
}
