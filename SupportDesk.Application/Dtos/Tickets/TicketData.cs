using SupportDesk.Domain.Tickets;

namespace SupportDesk.Application.Dtos.Tickets;

public record TicketData
(
        Guid TicketId,
        string Title,
        string Description,
        Guid CustomerId,
        TicketPriority TicketPriority,
        TicketStatus TicketStatus,
        DateTime CreatedAt
);


