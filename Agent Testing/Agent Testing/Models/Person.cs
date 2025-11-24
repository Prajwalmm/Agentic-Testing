namespace Agent_Testing.Models
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Occupation { get; set; } = string.Empty;

    }
}
