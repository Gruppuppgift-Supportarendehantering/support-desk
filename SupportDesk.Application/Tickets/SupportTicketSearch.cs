using SupportDesk.Domain.Tickets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SupportDesk.Application.Tickets;

public static class SupportTicketSearch
{
    public static List<SupportTicket> Search(
        IEnumerable<SupportTicket> tickets, 
        string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return tickets.ToList();
        }

        string searchTerm = searchText.Trim();

        return tickets.Where(t => t.Title.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) 
        ||
        t.Description.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
