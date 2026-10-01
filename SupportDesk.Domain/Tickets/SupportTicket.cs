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
}
