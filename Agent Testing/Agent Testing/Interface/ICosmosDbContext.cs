using Microsoft.Azure.Cosmos;

namespace Agent_Testing.Interface
{
    public interface ICosmosDbContext
    {
        /// <summary>
        /// Name of the database
        /// </summary>
        public string DatabaseName { get; init; }

        /// <summary>
        /// Cosmos client
        /// </summary>
        public CosmosClient CosmosClient { get; init; }
    }
}
