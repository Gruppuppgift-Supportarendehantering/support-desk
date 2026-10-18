namespace SupportDesk.Application.Dtos.Customers;

public record UpdateCustomerRequest
    (
        Guid CustomerId,
        string CustomerName,
        string EmailAddress
    );
