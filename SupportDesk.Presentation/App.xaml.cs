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
    private readonly ServiceProvider _serviceProvider;

    public IServiceProvider Services => _serviceProvider;

    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();

        services.AddApplication();
        services.AddInfrastructure();
        services.AddPresentation();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = _serviceProvider.GetRequiredService<MainWindow>();
        _window.Activate();
    }
}
