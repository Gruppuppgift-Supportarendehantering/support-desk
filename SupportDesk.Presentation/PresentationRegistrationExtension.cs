using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Presentation.Navigation;

namespace SupportDesk.Presentation;

public static class PresentationRegistrationExtension
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainWindow>();

        return services;
    }
}
