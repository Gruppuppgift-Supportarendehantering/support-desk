using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.Navigation;
using SupportDesk.Presentation.ViewModels.Tickets;


namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class SupportTicketView : Page
{
    private readonly INavigationService _navigationService;
    public SupportTicketOverviewViewModel ViewModel { get; } 



    public SupportTicketView()
    {
        var services = ((App)Microsoft.UI.Xaml.Application.Current).Services;
        _navigationService = services.GetRequiredService<INavigationService>();
        ViewModel = services.GetRequiredService<SupportTicketOverviewViewModel>();
        InitializeComponent();

        Loaded += SupportTicketView_Loaded;
    }

    private async void SupportTicketView_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadTicketsCommand.ExecuteAsync(null);
    }

    private void OpenTicketButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedTicket is null)
        {
            return;
        }
        Frame.Navigate(typeof(EditSupportTicketView), ViewModel.SelectedTicket); //ViewModel.SelectedTicket.Id efter merge?
    }
    private void CustomerViewButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SupportDesk.Presentation.Views.Customers.CustomerView));
    }

    private void CreateTicketButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(CreateSupportTicketView));
    }
}
