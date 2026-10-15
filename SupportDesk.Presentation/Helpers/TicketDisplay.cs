using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using SupportDesk.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Presentation.Helpers;

public static class TicketDisplay
{
    public static string StatusText(TicketStatus status) => status switch
    {
        TicketStatus.New => "Nytt",
        TicketStatus.InProgress => "Pågående",
        TicketStatus.Resolved => "Löst",
        _ => status.ToString()
    };

    public static SolidColorBrush StatusBrush(TicketStatus status) => status switch
    {
        TicketStatus.New => Solid(0x3B, 0x82, 0xF6),        // blå
        TicketStatus.InProgress => Solid(0xF5, 0x9E, 0x0B), // orange
        TicketStatus.Resolved => Solid(0x22, 0xC5, 0x5E),   // grön
        _ => Solid(0x6B, 0x72, 0x80)
    };

    public static string PriorityText(TicketPriority priority) => priority switch
    {
        TicketPriority.Low => "Låg",
        TicketPriority.Medium => "Medel",
        TicketPriority.High => "Hög",
        _ => priority.ToString()
    };

    public static SolidColorBrush PriorityBrush(TicketPriority priority) => priority switch
    {
        TicketPriority.Low => Solid(0x64, 0x74, 0x8B),    // grå
        TicketPriority.Medium => Solid(0xF5, 0x9E, 0x0B), // orange
        TicketPriority.High => Solid(0xEF, 0x44, 0x44),   // röd
        _ => Solid(0x6B, 0x72, 0x80)
    };

    private static SolidColorBrush Solid(byte r, byte g, byte b)
        => new(ColorHelper.FromArgb(255, r, g, b));
}
