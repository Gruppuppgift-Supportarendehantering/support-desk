using SupportDesk.Domain.Tickets;

namespace SupportDesk.Application.Dtos.Tickets;

public record SupportTicketRequest
(
string Title,
string Description,
Guid CustomerId,
TicketPriority TicketPriority
);
