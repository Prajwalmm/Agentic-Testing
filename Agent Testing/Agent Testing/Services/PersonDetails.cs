using Agent_Testing.Models;
using Agent_Testing.Repository;
using Agent_Testing.Repository.Interface;
using Agent_Testing.Services.Interface;
using System;

namespace Agent_Testing.Services
{
    public class PersonDetails : IPersonDetails
    {
        private readonly IPersonDataRepo _personDetailRepo;
        public PersonDetails(IPersonDataRepo personDetailRepo)
        {
            _personDetailRepo = personDetailRepo;
        }
        //private static readonly List<Person> People = new()
        //{
        //new Person { Name = "Alice", PhoneNumber = "1234567890", DateOfBirth = new DateOnly(1990, 5, 1), Age = 35 },
        //new Person { Name = "Bob", PhoneNumber = "9876543210", DateOfBirth = new DateOnly(1985, 12, 10), Age = 40 }
        //};
        //public async Task<Person> GetPersonDetails(string phonenumber)
        //{

        //    var result = People.FirstOrDefault(x => x.PhoneNumber == phonenumber);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    return new Person();
        //}
        public async Task<List<Person>> GetAllPersons()
        {
            return await _personDetailRepo.GetAllPersons();
        }
        public bool TestConnection()
        {
            return _personDetailRepo.TestConnection();
        }

        //public void CreatePersonTable()
        //{
        //    _personDetailRepo.CreatePersonTable();
        //}
    }
}
