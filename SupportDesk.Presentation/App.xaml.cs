using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SupportDesk.Application;
using SupportDesk.Infrastructure;
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

        services.AddApplication();
        services.AddInfrastructure();
        services.AddPresentation();

        Services = services.BuildServiceProvider();
    }

    protected override void OnLaunched(
        Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = Services.GetRequiredService<MainWindow>();
        _window.Activate();
    }
}