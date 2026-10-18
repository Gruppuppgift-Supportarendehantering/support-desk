namespace SupportDesk.Application.Dtos.Customers;

public record GetCustomersResponse
    (
        bool Succeeded,
        IReadOnlyList<CustomerData> Customers,
        string? ErrorMessage
    );
