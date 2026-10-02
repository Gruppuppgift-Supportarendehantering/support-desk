using SupportDesk.Application.Dtos.Customers;

namespace SupportDesk.Application.Customers;

public interface ICustomerService
{
    CustomerResponse CreateCustomer(CreateCustomerRequest request);
    CustomerResponse GetCustomerById(Guid customerId);
    GetCustomerResponse GetAllCustomers();
    CustomerResponse RenameCustomer(RenameCustomerRequest request);
    CustomerResponse ChangeEmailAddress(ChangeEmailAddressRequest request);
    DeleteCustomerResponse DeleteCustomer(Guid customerId);
}
