namespace SupportDesk.Application.Dtos.Customers;

public record ChangeEmailAddressRequest
    (
        Guid CustomerId,
        string EmailAddress
    );