using Agent_Testing.Models;

namespace Agent_Testing.Services.Interface
{
    public interface IPersonCosmosService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
        Task<Person> CreatePersonAsync(Person person);
        Task<Person?> UpdatePersonAsync(int id, Person person);
        Task<bool> DeletePersonAsync(int id);
    }
}
