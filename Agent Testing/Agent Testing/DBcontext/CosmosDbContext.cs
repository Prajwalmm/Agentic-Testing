using Agent_Testing.Interface;
using Microsoft.Azure.Cosmos;

namespace Agent_Testing.DBcontext
{
    public class CosmosDbContext : ICosmosDbContext
    {
        /// <summary>
        /// Cosmos Db Context Constructor
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="cosmosClient"></param>
        public CosmosDbContext(string databaseName, CosmosClient cosmosClient)
        {
            DatabaseName = databaseName;
            CosmosClient = cosmosClient;
        }

        public string DatabaseName { get; init; }
        public CosmosClient CosmosClient { get; init; }

    }
}
