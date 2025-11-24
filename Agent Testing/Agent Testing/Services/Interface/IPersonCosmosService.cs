using Agent_Testing.Models;

namespace Agent_Testing.Services.Interface
{
    public interface IPersonCosmosService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByPhNoAsync(string Phno);
        Task<Person> CreatePersonAsync(Person person);
        Task<Person?> UpdatePersonAsync(string id, Person person);
        Task<bool> DeletePersonAsync(string id);
    }
}
