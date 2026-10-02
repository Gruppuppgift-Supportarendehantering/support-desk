using SupportDesk.Application.Dtos.Customers;

namespace SupportDesk.Application.Customers;

public interface ICustomerService
{
    Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest request);
    Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId);
    Task<GetCustomerResponse> GetAllCustomersAsync();
    Task<CustomerResponse> RenameCustomerAsync(RenameCustomerRequest request);
    Task<CustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request);
    Task<CustomerResponse> ChangeEmailAddressAsync(ChangeEmailAddressRequest request);
    Task<DeleteCustomerResponse> DeleteCustomerAsync(Guid customerId);
}
