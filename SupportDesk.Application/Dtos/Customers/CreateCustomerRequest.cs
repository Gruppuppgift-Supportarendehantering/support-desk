namespace SupportDesk.Application.Dtos.Customers;

public record CreateCustomerRequest
    (
        string CustomerName,
        string EmailAddress,
        bool IsCompany
    );
