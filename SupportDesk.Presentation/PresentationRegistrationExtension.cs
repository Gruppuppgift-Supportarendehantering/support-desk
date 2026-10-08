using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Presentation.Navigation;
using SupportDesk.Presentation.ViewModels;
using SupportDesk.Presentation.ViewModels.Customers;
using SupportDesk.Presentation.ViewModels.Tickets;

namespace SupportDesk.Presentation;

public static class PresentationRegistrationExtension
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainWindow>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<CustomerViewModel>();
        services.AddTransient<CreateCustomerViewModel>();
        services.AddTransient<EditCustomerViewModel>();

        services.AddTransient<CreateSupportTicketViewModel>();

        return services;
    }
}
