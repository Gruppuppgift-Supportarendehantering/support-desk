using Microsoft.UI.Xaml.Data;
using SupportDesk.Domain.Tickets;
using System;

namespace SupportDesk.Presentation.Helpers;

public sealed class EnumTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => value switch
    {
        TicketStatus s => TicketDisplay.StatusText(s),
        TicketPriority p => TicketDisplay.PriorityText(p),
        _ => value?.ToString() ?? string.Empty
    };

    public object ConvertBack(object value, Type targetType, object parameter, string language)
        => throw new NotImplementedException();
}
