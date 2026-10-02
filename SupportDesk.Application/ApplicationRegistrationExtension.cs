using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;

namespace SupportDesk.Application;

public static class ApplicationRegistrationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();

        return services;
    }
}
