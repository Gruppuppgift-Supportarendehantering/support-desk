using SupportDesk.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Application.Dtos.Tickets
{
    public record SupportTicketResult
    (
        Guid TicketId,
        string Title,
        string Description,
        Guid CustomerId,
        TicketPriority TicketPriority,
        TicketStatus TicketStatus,
        DateTime CreatedAt
    
        
    );
}
