using Agent_Testing.Models;

namespace Agent_Testing.Repository.Interface
{
    public interface IPersonCosmosRepo
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByPhNoAsync(string PhNo);
        Task<Person> AddAsync(Person person);
        Task<Person?> UpdateAsync(string id, Person person);
        Task<bool> DeleteAsync(string id);
    }
}
