namespace SupportDesk.Application.Dtos.Customers;

public record GetCustomerResponse
    (
        bool Succeeded,
        IReadOnlyList<CustomerData> Customers,
        string? ErrorMessage
    );
