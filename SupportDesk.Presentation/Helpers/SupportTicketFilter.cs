using SupportDesk.Domain.Tickets;
using System.Collections.Generic;
using System.Linq;

namespace SupportDesk.Presentation.Helpers;

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
            result = result.Where(t => t.TicketStatus == status.Value);
        }

        if (priority.HasValue)
        {
            result = result.Where(t => t.TicketPriority == priority.Value);
        }

        return result.ToList();
    }
}
