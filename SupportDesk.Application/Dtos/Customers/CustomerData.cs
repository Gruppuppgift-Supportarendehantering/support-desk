namespace SupportDesk.Application.Dtos.Customers;

public record CustomerData
    (
        Guid CustomerId,
        string CustomerName,
        string EmailAddress,
        bool IsCompany
    )
{
    public string CustomerTypeText => IsCompany ? "Företagskund" : "Privatkund";
}
