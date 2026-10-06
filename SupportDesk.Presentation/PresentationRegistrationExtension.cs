using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Presentation.Navigation;
using SupportDesk.Presentation.ViewModels.Tickets;
using SupportDesk.Presentation.Views.Tickets;

namespace SupportDesk.Presentation;

public static class PresentationRegistrationExtension
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<CreateSupportTicketView>();
        services.AddTransient<CreateSupportTicketViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();

        return services;
    }
}
