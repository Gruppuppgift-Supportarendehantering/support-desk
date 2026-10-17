using SupportDesk.Domain.Tickets;
using System.Collections.Generic;
using System.Linq;

namespace SupportDesk.Application.Tickets;

public static class SupportTicketFilter
{
    public static List<SupportTicket> FilterByStatusAndPriority(
        IEnumerable<SupportTicket> tickets, 
        TicketStatus? status = null, 
        TicketPriority? priority = null)
    {
        var result = tickets;

        if (status.HasValue)
        {
            result = result.Where(t => t.Status == status.Value);
        }

        if (priority.HasValue)
        {
            result = result.Where(t => t.Priority == priority.Value);
        }

        return result.ToList();
    }
}
