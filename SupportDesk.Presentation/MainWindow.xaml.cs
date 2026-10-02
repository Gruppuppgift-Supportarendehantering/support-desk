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
        _navigationService.Navigate(AppPage.CreateCustomer);
    }
}
