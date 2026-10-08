using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using System.Collections.ObjectModel;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class EditSupportTicketViewModel : ObservableObject
{
    private readonly ISupportTicketService _supportTicketService;
    private readonly ICustomerService _customerService;

    public EditSupportTicketViewModel(ISupportTicketService supportTicketService, ICustomerService customerService)
    {
       _supportTicketService = supportTicketService;
       _customerService = customerService;

    }

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial CustomerData? SelectedCustomer { get; set; }

    [ObservableProperty]
    public partial TicketPriority TicketPriority { get; set; }

    public ObservableCollection<CustomerData> Customers { get; } = [];

}
