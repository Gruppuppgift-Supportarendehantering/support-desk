using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Domain.Tickets;

namespace SupportDesk.Application.Tickets;

public interface ISupportTicketService
{
    Task<SupportTicketResult> CreateTicketAsync(SupportTicketRequest request);
    Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync();
    Task<SupportTicketResult> UpdateTicketAsync(SupportTicket ticket);
}
