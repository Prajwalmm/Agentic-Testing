using Agent_Testing.Models;
using Agent_Testing.Repository.Interface;
using Agent_Testing.Services.Interface;

namespace Agent_Testing.Services
{
    public class PersonCosmosService : IPersonCosmosService
    {
        private readonly IPersonCosmosRepo _personCosmosRepo;

        public PersonCosmosService(IPersonCosmosRepo productRepository)
        {
            _personCosmosRepo = productRepository;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            try
            {
                var persons = await _personCosmosRepo.GetAllAsync();
                return persons;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            try
            {               
                var person = await _personCosmosRepo.GetByIdAsync(id);               
                return person;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            try
            {
                if (person == null)
                {
                    throw new ArgumentNullException(nameof(person), "Product cannot be null");
                }                       

                var createdProduct = await _personCosmosRepo.AddAsync(person);

                return createdProduct;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person?> UpdatePersonAsync(int id, Person person)
        {
            try
            {
                if (person == null)
                {
                    throw new ArgumentNullException(nameof(Person), "Product cannot be null");
                }

                var updatedPerson = await _personCosmosRepo.UpdateAsync(id, person);               

                return updatedPerson;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            try
            {         
                var result = await _personCosmosRepo.DeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }         

    }
}
