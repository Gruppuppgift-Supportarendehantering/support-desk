using SupportDesk.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Application.Tickets;

public interface ISupportTicketRepository //flytta över till domain lagret vid tillfälle. pga DDD.
{
    Task AddTicketToListAsync(SupportTicket ticket);

    Task<SupportTicket?> GetTicketbyIdAsync(Guid ticketId);

    Task<IEnumerable<SupportTicket>> GetAllTicketsAsync();

    Task UpdateTicketAsync(SupportTicket ticket);
    Task DeleteTicketAsync(Guid ticketId);
}
