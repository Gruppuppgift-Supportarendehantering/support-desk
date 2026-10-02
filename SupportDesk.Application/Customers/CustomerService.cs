using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Domain.Customers;
using SupportDesk.Domain.ValueObjects;

namespace SupportDesk.Application.Customers;

internal class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public CustomerResponse CreateCustomer(CreateCustomerRequest request)
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

        bool saved = customerRepository.Add(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be saved.");
        }

        var customerData = new CustomerData
            (
                customer.CustomerId,
                customer.CustomerName.Value,
                customer.EmailAddress.Value,
                customer.IsCompany
            );

        return new CustomerResponse(true, customerData, null);
    }
    public CustomerResponse GetCustomerById(Guid customerId)
    {
        var customer = customerRepository.GetById(customerId);

        if (customer is null)
        {
            return new CustomerResponse(false, null, "Customer was not found.");
        }

        var customerData = new CustomerData(
            customer.CustomerId,
            customer.CustomerName.Value,
            customer.EmailAddress.Value,
            customer.IsCompany);

        return new CustomerResponse(true, customerData, null);
    }
    public GetCustomerResponse GetAllCustomers()
    {
        var customers = customerRepository.GetAll();

        var customerdata = customers
            .Select(customer => new CustomerData(
                customer.CustomerId,
                customer.CustomerName.Value,
                customer.EmailAddress.Value,
                customer.IsCompany))
            .ToList();

        return new GetCustomerResponse(true, customerdata, null);
    }
    public CustomerResponse RenameCustomer(RenameCustomerRequest request)
    {
        throw new NotImplementedException();
    }

    public CustomerResponse ChangeEmailAddress(ChangeEmailAddressRequest request)
    {
        throw new NotImplementedException();
    }

    public DeleteCustomerResponse DeleteCustomer(Guid customerId)
    {
        throw new NotImplementedException();
    }

   
}
