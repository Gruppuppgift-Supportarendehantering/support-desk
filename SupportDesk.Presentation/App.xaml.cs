using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SupportDesk.Application;
using SupportDesk.Infrastructure;
using SupportDesk.Presentation.Navigation;
using System;

namespace SupportDesk.Presentation;

public partial class App : Microsoft.UI.Xaml.Application
{
    private Window? _window;
    public IServiceProvider Services { get; }
    
    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();

        services.AddInfrastructure();
        services.AddApplication();
        services.AddPresentation();

        Services = services.BuildServiceProvider();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var navigationService = Services.GetRequiredService<INavigationService>();
        
        
        //var navigationService = new NavigationService(Services);
        _window = new MainWindow(navigationService);
        _window.Activate();
    }
}
