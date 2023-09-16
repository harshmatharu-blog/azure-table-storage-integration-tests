using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureTableStorageIntegrationTests;

public class EmployeeRepositoryTests : IDisposable
{
    private readonly EmployeeRepository repository;

    public EmployeeRepositoryTests()
    {
        repository = CreateRepository($"EmployeeTest{Guid.NewGuid().ToString("N")}");
        repository.CreateTable();
    }

    private EmployeeRepository CreateRepository(string tableName)
    {
        var configValues = new Dictionary<string, string?>()
        {
            { "TableStorage:ConnectionString", "<connection string>"},
            { "TableStorage:Tables:Employee", tableName }

        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

        using var loggerFactory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Debug)
            .AddConsole());

        return new EmployeeRepository(config, loggerFactory.CreateLogger<EmployeeRepository>());
    }

    [Fact]
    public async Task Should_save_the_employee()
    {
        var employee = new Employee(Guid.NewGuid(), "FirstName", "LastName", "City", "Department");
        Assert.True(await repository.Add(employee));
    }

    [Fact]
    public async Task Should_return_the_saved_employee()
    {
        var e1 = new Employee(Guid.NewGuid(), "FirstName", "LastName", "City", "Department");
        await repository.Add(e1);

        var e2 = await repository.Get(e1.Department, e1.Id);
        
        Assert.NotNull(e2);
        Assert.Equal(e1.Id, e2.Id);
    }

    public void Dispose()
    {
        repository.DeleteTable();
    }
}