
using SupportDesk.Application.Dtos.Tickets;

namespace SupportDesk.Application.Tickets;

public interface ISupportTicketService
{
    Task<SupportTicketResult> CreateTicketAsync(SupportTicketRequest request);
}
