using Agent_Testing.Models;
using Agent_Testing.Repository.Interface;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Agent_Testing.Repository
{
    public class PersonCosmosRepo : IPersonCosmosRepo
    {
        private readonly Container _container;

        public PersonCosmosRepo(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:ContainerName"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            try
            {
                var query = _container.GetItemQueryIterator<Person>(
                    new QueryDefinition("SELECT * FROM c"));

                var results = new List<Person>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }

                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Person>(id.ToString(), new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person> AddAsync(Person product)
        {
            try
            {
                var response = await _container.CreateItemAsync(product, new PartitionKey(product.Id));
                return response.Resource;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person?> UpdateAsync(int id, Person product)
        {
            try
            {
                product.Id = id;
                var response = await _container.ReplaceItemAsync(product, id.ToString(), new PartitionKey(id));
                return response.Resource;
            }           
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _container.DeleteItemAsync<Person>(id.ToString(), new PartitionKey(id));
                return true;
            }            
            catch (Exception ex)
            {
                throw;
            }
        }      
    }
}
