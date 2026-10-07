
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class CreateSupportTicketViewModel : ObservableObject
{
    private readonly ISupportTicketService _supportTicketService;
    private readonly ICustomerService _customerService;

    public CreateSupportTicketViewModel(ICustomerService customerService, ISupportTicketService supportTicketService)
    {
        _supportTicketService = supportTicketService;
        _customerService = customerService;

        _ = LoadCustomersAsync();
    }

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SuccessMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial CustomerData? SelectedCustomer { get; set; }

    [ObservableProperty]
    public partial TicketPriority TicketPriority { get; set; }

    public ObservableCollection<CustomerData> Customers { get; } = [];

    public TicketPriority[] Priorities { get; } = Enum.GetValues<TicketPriority>();

    [RelayCommand]
    private void UICreateTicket()
    {
        SuccessMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Title))
        {
            ErrorMessage = "Titel får inte vara tom.";
            return;
        }
        if (SelectedCustomer == null)
        {
            ErrorMessage = "Välj en kund först.";
            return;
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = "Beskrivning får inte vara tom.";
            return;
        }
        var request = new SupportTicketRequest(Title, Description, SelectedCustomer.CustomerId, TicketPriority);

        SupportTicketResult result = _supportTicketService.CreateTicket(request);

        ErrorMessage = string.Empty;
        SuccessMessage = $"Ärendet har skapats! Med ärendenummer {result.TicketId}";

        Title = string.Empty;
        Description = string.Empty;
        SelectedCustomer = null;
        TicketPriority = TicketPriority.Normal;

    }


    public async Task LoadCustomersAsync()
    {
        try
        {
            var response = await _customerService.GetAllCustomersAsync();

            if (!response.Succeeded)
            {
                ErrorMessage = response.ErrorMessage ?? "Kunde inte hämta kunder.";
                return;
            }

            Customers.Clear();

            foreach (var customer in response.Customers)
            {
                Customers.Add(customer);
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
        {
            ErrorMessage = "Kunderna kunde inte läsas in";
        }
    }
}
