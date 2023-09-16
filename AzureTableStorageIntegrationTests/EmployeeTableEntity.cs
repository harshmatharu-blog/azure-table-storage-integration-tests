using Azure;
using Azure.Data.Tables;

namespace AzureTableStorageIntegrationTests;

public class EmployeeTableEntity : ITableEntity
{
    public EmployeeTableEntity() { }

    public EmployeeTableEntity(string partitionKey, string rowKey, Employee employee)
    {
        Id = employee.Id;
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        City = employee.City;
        Department = employee.Department;

        PartitionKey = partitionKey;
        RowKey = rowKey;
    }

    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string City { get; set; }

    public string Department { get; set; }

    public string PartitionKey { get; set; }

    public string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }
}
