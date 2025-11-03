using Agent_Testing.Models;

namespace Agent_Testing.Repository.Interface
{
    public interface IPersonCosmosRepo
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person> AddAsync(  Person person);
        Task<Person?> UpdateAsync(int id, Person person);
        Task<bool> DeleteAsync(int id);
    }
}
