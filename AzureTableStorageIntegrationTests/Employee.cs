namespace AzureTableStorageIntegrationTests;

public class Employee
{
    public Employee(Guid id, string firstName, string lastName, string city, string department)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        City = city;
        Department = department;
    }

    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string City { get; set; }

    public string Department { get; set; }
}
