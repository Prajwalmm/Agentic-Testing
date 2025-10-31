using Agent_Testing.Models;
using Agent_Testing.Repository.Interface;
using IBM.Data.Db2;
using Microsoft.Extensions.Configuration;

namespace Agent_Testing.Repository
{
    public class PersonDataRepo : IPersonDataRepo
    {
        private readonly string _connectionString;
        public PersonDataRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AS400Connection") ?? "";
        }
        public async Task<List<Person>> GetAllPersons()
        {
            var persons = new List<Person>();

            await Task.Run(() =>  
            {
                try
                {
                    using (var connection = new DB2Connection(_connectionString))
                    {
                        connection.Open();  

                        string query = "SELECT * FROM PRAJWALMM1.PERSON";

                        using (var command = new DB2Command(query, connection))
                        using (var reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())  
                            {
                                persons.Add(new Person
                                {
                                    Name = reader["NAME"].ToString() ?? "",
                                    Age = Convert.ToInt32(reader["AGE"]),
                                    DateOfBirth = DateOnly.TryParse(reader["DATEOFBIRTH"]?.ToString(), out var dob)
                                        ? dob
                                        : new DateOnly(1900, 1, 1),
                                    PhoneNumber = reader["PHONENUMBER"].ToString() ?? ""
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data: {ex.Message}");
                    throw;
                }
            });

            return persons;
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = new DB2Connection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        public void CreatePersonTable()
        {
            try
            {
                using (var connection = new DB2Connection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully!");

                    string query = @"
                        CREATE TABLE PRAJWALMM.PERSON (
                            NAME VARCHAR(50),
                            AGE INTEGER,
                            DATEOFBIRTH DATE,
                            PHONENUMBER VARCHAR(15)
                        )
                    ";

                    using (var cmd = new DB2Command(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("PERSON table created successfully in AS400!");
            }
            catch (DB2Exception ex)
            {
                Console.WriteLine($"DB2 Error: {ex.Message}");
                throw new Exception($"Error while creating table: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }
    }
}
