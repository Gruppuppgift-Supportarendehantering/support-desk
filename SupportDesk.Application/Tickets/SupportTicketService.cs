using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Domain.Tickets;
using System.Reflection;
using System.Text.Json;

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

        try
        {
            await _repository.AddTicketToJsonAsync(ticket);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException or KeyNotFoundException)
        {
            return new SupportTicketResult(false, null, "Kunde inte spara ärendet. Försök igen.");
        }

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
    public async Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync()
    {
        return await _repository.GetAllTicketsAsync();
    }
    public async Task<SupportTicketResult> UpdateTicketAsync(SupportTicket ticket)
    {
        try
        {
            await _repository.UpdateTicketAsync(ticket);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException or KeyNotFoundException)
        {
            return new SupportTicketResult(false, null, "Kunde inte spara ärendet. Försök igen.");
        }

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
