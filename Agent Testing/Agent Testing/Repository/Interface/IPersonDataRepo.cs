using Agent_Testing.Models;

namespace Agent_Testing.Repository.Interface
{
    public interface IPersonDataRepo
    {
        void CreatePersonTable();
        Task<List<Person>> GetAllPersons();
        bool TestConnection(); 

    }
}
