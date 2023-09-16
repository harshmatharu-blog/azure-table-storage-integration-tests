using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureTableStorageIntegrationTests;

public class EmployeeRepository
{
    private readonly TableClient client;
    private readonly ILogger<EmployeeRepository> logger;

    public EmployeeRepository(IConfiguration config, ILogger<EmployeeRepository> logger)
    {
        client = new TableClient(config["TableStorage:ConnectionString"], config["TableStorage:Tables:Employee"]);
        this.logger = logger;
    }

    public async Task<bool> Add(Employee employee)
    {
        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        var employeeEntity = new EmployeeTableEntity(employee.Department, employee.Id.ToString(), employee);
        try
        {
            await client.AddEntityAsync(employeeEntity);
            return true;
        }
        catch (Azure.RequestFailedException ex)
        {
            logger.LogError(ex, "Not able to save entity.");
            return false;
        }
    }

    public async Task<Employee?> Get(string department, Guid id)
    {
        var response = await client.GetEntityIfExistsAsync<EmployeeTableEntity>(department, id.ToString());
        if (!response.HasValue)
        {
            return null;
        }

        var e = response.Value;
        return new Employee(e.Id, e.FirstName, e.LastName, e.City, e.Department);
    }

    public void CreateTable() => client.Create();

    public void DeleteTable() => client.Delete();
}
