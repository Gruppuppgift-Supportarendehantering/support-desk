using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Domain.Customers;
using SupportDesk.Domain.ValueObjects;

namespace SupportDesk.Application.Customers;

internal class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Customer customer;

        try
        {
            var customerId = Guid.NewGuid();

            customer = new Customer
                (
                    customerId,
                    new Name(request.CustomerName),
                    new EmailAddress(request.EmailAddress),
                    request.IsCompany
                );
        }
        catch (ArgumentException ex)
        {
            return new CustomerResponse(false, null, ex.Message);
        }

        bool saved = await customerRepository.CreateAsync(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be saved.");
        }

        return new CustomerResponse(true, ToCustomerData(customer), null);
    }
    public async Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            return new CustomerResponse(false, null, "Customer was not found.");
        }

        return new CustomerResponse(true, ToCustomerData(customer), null);
    }
    public async Task<GetCustomerResponse> GetAllCustomersAsync()
    {
        var customers = await customerRepository.GetAllAsync();

        var customerData = customers.Select(ToCustomerData).ToList();

        return new GetCustomerResponse(true, customerData, null);
    }
    public async Task<CustomerResponse> RenameCustomerAsync(RenameCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = await customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            return new CustomerResponse(false, null, "Customer was not found.");
        }

        try
        {
            customer.Rename(new Name(request.CustomerName));
        }
        catch (ArgumentException ex)
        {
            return new CustomerResponse(false, null, ex.Message);
        }

        bool saved = await customerRepository.UpdateAsync(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be updated.");
        }

        return new CustomerResponse(true, ToCustomerData(customer), null);
    }

    public async Task<CustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = await customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            return new CustomerResponse(false, null, "Customer was not found.");
        }

        Name name;
        EmailAddress emailAddress;

        try
        {
            name = new Name(request.CustomerName);
            emailAddress = new EmailAddress(request.EmailAddress);
        }
        catch (ArgumentException ex)
        {
            return new CustomerResponse(false, null, ex.Message);
        }

        customer.Rename(name);
        customer.ChangeEmailAddress(emailAddress);

        bool saved = await customerRepository.UpdateAsync(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be updated.");
        }

        return new CustomerResponse(true, ToCustomerData(customer), null);
    }

    public async Task<CustomerResponse> ChangeEmailAddressAsync(ChangeEmailAddressRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = await customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            return new CustomerResponse(false, null, "Customer was not found.");
        }

        try
        {
            customer.ChangeEmailAddress(new EmailAddress(request.EmailAddress));
        }
        catch (ArgumentException ex)
        {
            return new CustomerResponse(false, null, ex.Message);
        }

        bool saved = await customerRepository.UpdateAsync(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be updated.");
        }

        return new CustomerResponse(true, ToCustomerData(customer), null);
    }

    public async Task<DeleteCustomerResponse> DeleteCustomerAsync(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            return new DeleteCustomerResponse(false, "Customer ID is required.");
        }

        bool deleted = await customerRepository.DeleteAsync(customerId);

        return deleted
            ? new DeleteCustomerResponse(true, null)
            : new DeleteCustomerResponse(false, "Customer was not found.");
    }

    private static CustomerData ToCustomerData(Customer customer)
    {
        return new CustomerData(
            customer.CustomerId,
            customer.CustomerName.Value,
            customer.EmailAddress.Value,
            customer.IsCompany);
    }


}
