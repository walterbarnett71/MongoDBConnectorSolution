using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBConnectorLib
{
    public class MongoDBConnector
    {
        private readonly MongoClient _client;

        // Constructor that takes a connection string
        public MongoDBConnector(string connectionString)
        {
            _client = new MongoClient(connectionString);
        }

        // Method that pings MongoDB
        public bool Ping()
        {
            try
            {
                var db = _client.GetDatabase("admin");
                var cmd = new BsonDocument("ping", 1);
                db.RunCommand<BsonDocument>(cmd);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
