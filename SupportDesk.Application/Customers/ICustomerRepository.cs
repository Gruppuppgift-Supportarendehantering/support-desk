using SupportDesk.Domain.Customers;

namespace SupportDesk.Application.Customers;

public interface ICustomerRepository
{
    bool Add(Customer customer);
    Customer? GetById(Guid customerId);
    IReadOnlyList<Customer> GetAll();
    bool Update(Customer customer);
    bool Delete(Guid customerId);
}
