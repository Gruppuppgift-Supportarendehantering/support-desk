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
        ArgumentNullException.ThrowIfNull(request);

        var customer = customerRepository.GetById(request.CustomerId);

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

        bool saved = customerRepository.Update(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be updated.");
        }

        var customerData = new CustomerData(
            customer.CustomerId,
            customer.CustomerName.Value,
            customer.EmailAddress.Value,
            customer.IsCompany);

        return new CustomerResponse(true, customerData, null);
    }

    public CustomerResponse ChangeEmailAddress(ChangeEmailAddressRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = customerRepository.GetById(request.CustomerId);

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

        bool saved = customerRepository.Update(customer);

        if (!saved)
        {
            return new CustomerResponse(false, null, "Customer could not be updated.");
        }

        var customerData = new CustomerData(
            customer.CustomerId,
            customer.CustomerName.Value,
            customer.EmailAddress.Value,
            customer.IsCompany);

        return new CustomerResponse(true, customerData, null);
    }

    public DeleteCustomerResponse DeleteCustomer(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            return new DeleteCustomerResponse(false, "Customer ID is required.");
        }

        bool deleted = customerRepository.Delete(customerId);

        return deleted
            ? new DeleteCustomerResponse(true, null)
            : new DeleteCustomerResponse(false, "Customer was not found.");
    }


}
