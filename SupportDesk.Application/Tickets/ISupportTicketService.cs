
using SupportDesk.Application.Dtos.Tickets;

namespace SupportDesk.Application.Tickets;

public interface ISupportTicketService
{
    SupportTicketResult CreateTicket(SupportTicketRequest request);
}
