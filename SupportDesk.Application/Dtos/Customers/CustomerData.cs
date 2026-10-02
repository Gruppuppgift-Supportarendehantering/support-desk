namespace SupportDesk.Application.Dtos.Customers;

public record CustomerData
    (
        Guid CustomerId,
        string CustomerName,
        string EmailAddress,
        bool IsCompany
    );
