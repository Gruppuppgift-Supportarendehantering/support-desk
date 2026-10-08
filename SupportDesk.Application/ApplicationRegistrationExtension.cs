using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;

namespace SupportDesk.Application;

public static class ApplicationRegistrationExtension
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddSingleton<ISupportTicketService, SupportTicketService>();

        return services;
    }
}
