using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Presentation.Navigation;
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

        services.AddTransient<CustomerViewModel>();
        services.AddTransient<CreateCustomerViewModel>();
        services.AddTransient<EditCustomerViewModel>();

        services.AddTransient<CreateSupportTicketViewModel>();
        services.AddTransient<SupportTicketOverviewViewModel>();

        return services;
    }
}
