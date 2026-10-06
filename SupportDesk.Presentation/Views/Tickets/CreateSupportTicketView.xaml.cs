using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Tickets;


namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class CreateSupportTicketView : Page
{
    public CreateSupportTicketViewModel ViewModel { get; } = new();

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
        {
            Frame.GoBack();
            return;
        }

        Frame.Navigate(typeof(SupportTicketView));
    }

    public CreateSupportTicketView()
    {
        InitializeComponent();
    }
}
