using Agent_Testing.DBcontext;
using Agent_Testing.Models;
using Agent_Testing.Repository.Interface;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Agent_Testing.Repository
{
    public class PersonCosmosRepo : IPersonCosmosRepo
    {
        private readonly Container _container;
        private readonly CosmosDbContext _dbContext;
        public PersonCosmosRepo(CosmosClient cosmosClient, IConfiguration configuration, CosmosDbContext dbContext)
        {
            _dbContext = dbContext;
            _container = _dbContext.CosmosClient.GetContainer("iseries-to-cosmos", "Person");
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

        public async Task<Person?> GetByPhNoAsync(string phNo)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.phoneNumber = @phone")
                     .WithParameter("@phone", phNo);

                var iterator = _container.GetItemQueryIterator<Person>(query);
                List<Person> results = new List<Person>();

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    results.AddRange(response.Resource);
                }

                return results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Person> AddAsync(Person person)
        {
            try
            {
                var response = await _container.CreateItemAsync<Person>(person, new PartitionKey(person.Occupation));
                return response.Resource;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person?> UpdateAsync(string occupation, Person Data)
        {
            try
            {
                var response = await _container.UpsertItemAsync<Person>(Data, new PartitionKey(occupation));
                return response.Resource;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
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
