using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Presentation.Navigation;
using SupportDesk.Presentation.ViewModels.Customers;

namespace SupportDesk.Presentation;

public static class PresentationRegistrationExtension
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainWindow>();

        services.AddTransient<CreateCustomerViewModel>();

        return services;
    }
}
