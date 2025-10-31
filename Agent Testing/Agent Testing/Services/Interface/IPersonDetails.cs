using Agent_Testing.Models;

namespace Agent_Testing.Services.Interface
{
    public interface IPersonDetails
    {
        //Task<Person> GetPersonDetails(string phno);
        Task<List<Person>> GetAllPersons();
        bool TestConnection(); 
    }
}
