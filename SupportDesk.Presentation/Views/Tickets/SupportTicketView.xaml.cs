using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SupportDesk.Presentation.ViewModels.Tickets;


namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class SupportTicketView : Page
{
    public SupportTicketOverviewViewModel ViewModel { get; } = new();
    public SupportTicketView()
    {
        InitializeComponent();
    }
}
