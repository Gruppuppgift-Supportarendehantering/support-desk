namespace SupportDesk.Application.Dtos.Customers;

public record CustomerResponse
    (
        bool Succeeded,
        CustomerData? Customer,
        string? ErrorMessage
    );
