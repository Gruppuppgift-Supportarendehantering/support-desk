
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Tickets;
namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class EditSupportTicketView : Page
{
    public EditSupportTicketViewModel ViewModel { get; } = new();
    public EditSupportTicketView()
    {
        InitializeComponent();
    }
}
