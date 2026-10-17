using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Domain.Tickets;
using System.Reflection;

namespace SupportDesk.Application.Tickets;

public class SupportTicketService : ISupportTicketService
{
    private readonly ISupportTicketRepository _repository;
    private readonly ICustomerRepository _customerRepository;


    public SupportTicketService(ISupportTicketRepository repository, ICustomerRepository customerRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
    }


    public async Task<SupportTicketResult> CreateTicketAsync(SupportTicketRequest request)
    {
        if (request.CustomerId == Guid.Empty)
        {
            return new SupportTicketResult(false, null, "Du måste välja en kund.");
                
        }

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null)
        {
            return new SupportTicketResult(false, null, "Kunden hittades inte. Välj en registrerad kund.");
        }

        SupportTicket ticket = new SupportTicket
            (
            request.CustomerId,
            request.Title,
            request.Description,
            request.TicketPriority
            );

        await _repository.AddTicketToJsonAsync(ticket);

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
    public async Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync()
    {
        return await _repository.GetAllTicketsAsync();
    }
    public async Task<SupportTicketResult> UpdateTicketAsync(SupportTicket ticket)
    {
        await _repository.UpdateTicketAsync(ticket);

        TicketData ticketData = new TicketData(
            ticket.Id,
            ticket.Title,
            ticket.Description,
            ticket.CustomerId,
            ticket.Priority,
            ticket.Status,
            ticket.CreatedAt
            );

        return new SupportTicketResult(true, ticketData, null);
    }
}
