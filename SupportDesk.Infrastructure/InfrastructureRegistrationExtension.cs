using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Tickets;
using SupportDesk.Infrastructure.Tickets;
using System.Runtime.CompilerServices;

namespace SupportDesk.Infrastructure;

public static class InfrastructureRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISupportTicketRepository, SupportTicketRepository>();

        return services;
    }
}
