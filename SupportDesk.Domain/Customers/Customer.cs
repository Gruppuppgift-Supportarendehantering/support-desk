using SupportDesk.Domain.ValueObjects;

namespace SupportDesk.Domain.Customers;

public class Customer(Guid customerId, Name customerName, EmailAddress emailAddress, bool isCompany)
{
    public Guid CustomerId { get; } = customerId;
    public Name CustomerName { get; private set; } = customerName;
    public EmailAddress EmailAddress { get; private set; } = emailAddress;
    public bool IsCompany { get; } = isCompany;

    public void Rename(Name customerName)
    {
        CustomerName = customerName;
    }

    public void ChangeEmailAddress(EmailAddress emailAddress)
    {
        EmailAddress = emailAddress;
    }
}
