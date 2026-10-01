using SupportDesk.Domain.Customers;

namespace SupportDesk.Domain.Tickets;

public class SupportTicket
{
    public Guid TicketId { get; private set; }
    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public Guid CustomerId { get; private set; }

    public TicketPriority TicketPriority { get; private set; }

    //public TicketComment? TicketComment { get; set; }

    public TicketStatus TicketStatus { get; private set; } //sätt som New vid skapning

    public DateTime CreatedAt { get; private set; }

    public SupportTicket(string title, string description, Guid customerId, TicketPriority ticketpriority)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required");
        }
        if (customerId == Guid.Empty)
        {
            throw new ArgumentException("Customer is required");
        }

        TicketId = Guid.NewGuid();
        Title = title;
        Description = description;
        CustomerId = customerId;
        TicketPriority = ticketpriority;
        TicketStatus = TicketStatus.New;
        CreatedAt = DateTime.Now;
    }
}

public record CreateSupportTicketRequest //flytta till dto mappen i application
    (
    string Title,
    string Description,
    Guid CustomerId,
    TicketPriority TicketPriority
    );