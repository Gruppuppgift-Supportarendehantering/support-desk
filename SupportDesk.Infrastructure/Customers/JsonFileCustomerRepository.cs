using SupportDesk.Application.Customers;
using SupportDesk.Domain.Customers;
using SupportDesk.Domain.ValueObjects;
using System.Text.Json;

namespace SupportDesk.Infrastructure.Customers;

internal class JsonFileCustomerRepository : ICustomerRepository
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

    public async Task<bool> CreateAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var customers = await ReadCustomersAsync();

        if (customers.Any(c => c.CustomerId == customer.CustomerId))
        {
            return false;
        }

        var storageModel = new CustomerStorageModel(
            customer.CustomerId,
            customer.CustomerName.Value,
            customer.EmailAddress.Value,
            customer.IsCompany
            );

        customers.Add(storageModel);

        await SaveCustomersAsync(customers);

        return true;
    }

    public async Task<Customer?> GetByIdAsync(Guid customerId)
    {
        var customers = await ReadCustomersAsync();

        var storageModel = customers.FirstOrDefault(c => c.CustomerId == customerId);

        if (storageModel is null)
        {
            return null;
        }

        return ToCustomerObject(storageModel);
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        var customers = await ReadCustomersAsync();

        return customers.Select(ToCustomerObject).ToList();
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var customers = await ReadCustomersAsync();

        int index = customers.FindIndex(c => c.CustomerId == customer.CustomerId);

        if (index == -1)
        {
            return false;
        }

        customers[index] = new CustomerStorageModel(
            customer.CustomerId,
            customer.CustomerName.Value,
            customer.EmailAddress.Value,
            customer.IsCompany
            );

        await SaveCustomersAsync(customers);

        return true;

    }

    public async Task<bool> DeleteAsync(Guid customerId)
    {
        var customers = await ReadCustomersAsync();

        int removed = customers.RemoveAll(c => c.CustomerId == customerId);

        if (removed == 0)
        {
            return false;
        }

        await SaveCustomersAsync(customers);

        return true;
    }

    private async Task<List<CustomerStorageModel>> ReadCustomersAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        string json = await File.ReadAllTextAsync(_filePath);

        var customers = JsonSerializer.Deserialize<List<CustomerStorageModel>>(json, _options)
            ?? throw new JsonException("Customer file does not contain a valid customer list.");

        return customers;
    }

    private async Task SaveCustomersAsync(IEnumerable<CustomerStorageModel> customers)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        string json = JsonSerializer.Serialize(customers, _options);

        var tempPath = _filePath + ".tmp";

        await File.WriteAllTextAsync(tempPath, json);

        File.Move(tempPath, _filePath, overwrite: true);
    }

    private static Customer ToCustomerObject(CustomerStorageModel storageModel)
    {
        return new Customer(
            storageModel.CustomerId,
            new Name(storageModel.CustomerName),
            new EmailAddress(storageModel.EmailAddress),
            storageModel.IsCompany);
    }
}
