using Microsoft.UI.Xaml;
using SupportDesk.Presentation.Navigation;

namespace SupportDesk.Presentation;

public partial class App : Microsoft.UI.Xaml.Application
{
    private Window? _window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var navigationService = new NavigationService();
        _window = new MainWindow(navigationService);
        _window.Activate();
    }
}
