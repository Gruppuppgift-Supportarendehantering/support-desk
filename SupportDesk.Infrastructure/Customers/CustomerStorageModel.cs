namespace SupportDesk.Infrastructure.Customers;

internal record CustomerStorageModel
    (
        Guid CustomerId,
        string CustomerName,
        string EmailAddress,
        bool IsCompany
    );
