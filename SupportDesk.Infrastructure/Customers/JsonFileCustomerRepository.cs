using SupportDesk.Application.Customers;
using SupportDesk.Domain.Customers;
using System.Text.Json;

namespace SupportDesk.Infrastructure.Customers;

public class JsonFileCustomerRepository : ICustomerRepository
{
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
}
