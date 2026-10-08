using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SupportDesk.Presentation.ViewModels.Tickets;


namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class SupportTicketView : Page
{
    public SupportTicketOverviewViewModel ViewModel { get; } = new();

    private void OpenTicketButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedTicket is null)
        {
            return;
        }
        Frame.Navigate(typeof(EditSupportTicketView), ViewModel.SelectedTicket); //ViewModel.SelectedTicket.Id efter merge?
    }
    private void CreateTicketButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(CreateSupportTicketView));
    }

    private void CustomerViewButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SupportDesk.Presentation.Views.Customers.CustomerView));
    }

    public SupportTicketView()
    {
        InitializeComponent();
    }
}
