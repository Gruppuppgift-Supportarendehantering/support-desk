using SupportDesk.Domain.Customers;

namespace SupportDesk.Application.Customers;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid customerId);
    Task<IReadOnlyList<Customer>> GetAllAsync();
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(Guid customerId);
}
