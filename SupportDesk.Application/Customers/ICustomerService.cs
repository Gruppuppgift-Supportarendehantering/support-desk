using SupportDesk.Application.Dtos.Customers;

namespace SupportDesk.Application.Customers;

public interface ICustomerService
{
    CustomerResponse CreateCustomer(CreateCustomerRequest request);
    CustomerResponse RenameCustomer(RenameCustomerRequest request);
    CustomerResponse ChangeEmailAddress(ChangeEmailAddressRequest request);
    GetCustomerResponse GetAllCustomers();
}
