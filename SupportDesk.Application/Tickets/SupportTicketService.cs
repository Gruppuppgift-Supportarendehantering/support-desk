using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Domain.Tickets;
using System.Reflection;

namespace SupportDesk.Application.Tickets;

public class SupportTicketService : ISupportTicketService
{
    private readonly ISupportTicketRepository _repository;


    public SupportTicketService(ISupportTicketRepository repository)
    {
        _repository = repository;
    }


    public SupportTicketResult CreateTicket(SupportTicketRequest request)
    {
        SupportTicket ticket = new SupportTicket
            (
            request.CustomerId,
            request.Title,
            request.Description,
            request.TicketPriority
            );

        _repository.AddTicketToList(ticket);
        

        Guid ticketId = ticketId = ticket.Id;
        string  title = ticket.Title;
        string description = ticket.Description;
        Guid customerId = ticket.CustomerId;
        TicketPriority ticketPriority = ticket.Priority;
        TicketStatus ticketStatus = ticket.Status;
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
