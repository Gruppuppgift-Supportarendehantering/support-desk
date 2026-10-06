using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Tickets;


namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class CreateSupportTicketView : Page
{
    public CreateSupportTicketViewModel ViewModel { get; } = new();

    public CreateSupportTicketView()
    {
        InitializeComponent();
    }
}
