using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;
using SupportDesk.Infrastructure.Customers;

namespace SupportDesk.Infrastructure;

public static class InfrastructureRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, JsonFileCustomerRepository>();

        return services;
    }
}
