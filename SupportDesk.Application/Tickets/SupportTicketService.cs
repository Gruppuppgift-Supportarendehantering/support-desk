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


    public async Task<SupportTicketResult> CreateTicketAsync(SupportTicketRequest request)
    {
        SupportTicket ticket = new SupportTicket
            (
            request.CustomerId,
            request.Title,
            request.Description,
            request.TicketPriority
            );

        await _repository.AddTicketToListAsync(ticket);

        TicketData ticketData = new TicketData(
            ticket.Id,
            ticket.Title,
            ticket.Description,
            ticket.CustomerId,
            ticket.Priority,
            ticket.Status,
            ticket.CreatedAt
            );
        

        return new SupportTicketResult
            (
            true,
            ticketData,
            null
            );
    }
}
