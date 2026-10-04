using SupportDesk.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Application.Tickets;

public interface ISupportTicketRepository //flytta över till domain lagret vid tillfälle. pga DDD.
{
    void AddTicketToList(SupportTicket ticket);

    SupportTicket? GetbyId(Guid ticketId);

    IEnumerable<SupportTicket> GetAllTickets();
}
