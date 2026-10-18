using SupportDesk.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Application.Dtos.Tickets
{
    public record SupportTicketResult
    (
       bool Succeeded,
       TicketData? Ticket,
       string? ErrorMessage
    );
}
