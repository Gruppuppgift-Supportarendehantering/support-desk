using SupportDesk.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Application.Tickets;

public interface ISupportTicketRepository //flytta över till domain lagret vid tillfälle. pga DDD.
{
    Task AddAsync(SupportTicket ticket);

    Task<SupportTicket?> GetByIdAsync(Guid ticketId);

    Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync();
}
