namespace SupportDesk.Application.Dtos.Customers;

public record RenameCustomerRequest
    (
        Guid CustomerId,
        string CustomerName
    );
