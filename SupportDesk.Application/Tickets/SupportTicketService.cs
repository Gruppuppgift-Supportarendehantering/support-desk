using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Domain.Tickets;
using System.Reflection;

namespace SupportDesk.Application.Tickets;

public class SupportTicketService : ISupportTicketService
{
    public SupportTicketResult CreateTicket(SupportTicketRequest request)
    {
        SupportTicket ticket = new SupportTicket
            (
            request.Title, 
            request.Description, 
            request.CustomerId, 
            request.TicketPriority
            );

        Guid ticketId = ticket.TicketId;
        string  title = ticket.Title;
        string description = ticket.Description;
        Guid customerId = ticket.CustomerId;
        TicketPriority ticketPriority = ticket.TicketPriority;
        TicketStatus ticketStatus = ticket.TicketStatus;
        DateTime createdAt = ticket.CreatedAt;

        return new SupportTicketResult
            (
            ticketId,
            title,
            description,
            customerId,
            ticketPriority,
            ticketStatus,
            createdAt
            );
    }
}
