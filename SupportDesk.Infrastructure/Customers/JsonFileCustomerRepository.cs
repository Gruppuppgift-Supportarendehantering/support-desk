using SupportDesk.Application.Customers;
using SupportDesk.Domain.Customers;
using System.Text.Json;

namespace SupportDesk.Infrastructure.Customers;

public class JsonFileCustomerRepository : ICustomerRepository
{
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SupportDesk",
        "customers.json"
        );

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public Task<bool> CreateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    private async Task<List<CustomerStorageModel>> ReadCustomersAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        string json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<CustomerStorageModel>>(json, _options) 
            ?? throw new JsonException("Customer file does not contain a valid customer list.");
    }

    private async Task SaveCustomersAsync(IEnumerable<CustomerStorageModel> customers)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        string json = JsonSerializer.Serialize(customers, _options);

        var tempPath = _filePath + ".tmp";

        await File.WriteAllTextAsync(tempPath, json);
        File.Move(tempPath, _filePath, overwrite: true);
    }
}
