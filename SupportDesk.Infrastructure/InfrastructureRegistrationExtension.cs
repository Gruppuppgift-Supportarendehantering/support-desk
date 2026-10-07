using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;
using SupportDesk.Infrastructure.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Infrastructure.Tickets;

public class InfrastructureRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, JsonFileCustomerRepository>();
        services.AddSingleton<ISupportTicketRepository, SupportTicketRepository>();

        return services;

    }
}
