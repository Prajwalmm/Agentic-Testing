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

        public async Task<Person?> GetPersonByPhNoAsync(string PhNo)
        {
            try
            {
                var person = await _personCosmosRepo.GetByPhNoAsync(PhNo);
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
                    throw new ArgumentNullException(nameof(person), "Person Details cannot be empty or null");
                }

                var createdProduct = await _personCosmosRepo.AddAsync(person);

                return createdProduct;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Person?> UpdatePersonAsync(string phNo, Person updatedData)
        {
            try
            {
                if (updatedData == null)
                {
                    throw new ArgumentNullException(nameof(Person), "Person Details cannot be empty or null");
                }
                var person = await _personCosmosRepo.GetByPhNoAsync(phNo);
                if (person == null)
                    throw new Exception("Person not found");

                person.DateOfBirth = updatedData.DateOfBirth;
                person.PhoneNumber = updatedData.PhoneNumber;
                person.Occupation = updatedData.Occupation;
                person.Name = updatedData.Name;
                person.Age = updatedData.Age;
                var updatedPerson = await _personCosmosRepo.UpdateAsync(person.Occupation, person);
                return updatedPerson;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeletePersonAsync(string id)
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
