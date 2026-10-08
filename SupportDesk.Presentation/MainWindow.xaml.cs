using Microsoft.UI.Xaml;
using SupportDesk.Presentation.Navigation;

namespace SupportDesk.Presentation;

public sealed partial class MainWindow : Window
{
    private readonly INavigationService _navigationService;

    public MainWindow(INavigationService navigationService)
    {
        InitializeComponent();

        _navigationService = navigationService;
        _navigationService.Initialize(RootFrame);

        // Börja på kundlistan.
        _navigationService.Navigate(AppPage.Customers);
    }

    private void Customers_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.Navigate(AppPage.Customers);
    }

    private void CreateSupportTicket_Click(
        object sender, RoutedEventArgs e)
    {
        _navigationService.Navigate(AppPage.CreateSupportTicket);
    }
}