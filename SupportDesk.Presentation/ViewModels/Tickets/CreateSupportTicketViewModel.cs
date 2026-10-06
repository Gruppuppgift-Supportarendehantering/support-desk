using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Domain.Tickets;
using System;
using System.Collections.ObjectModel;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class CreateSupportTicketViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string SelectedCustomerName { get; set; } = string.Empty;
    public ObservableCollection<string> CustomerOptions { get; } = new()
    {
        "Dennis Brofalk",
        "Simon Öbom",
        "Leon Minnsej"
    };
    //senare med koppling till kunddatabas, men nu för demo purposes
    /*
     * public async Task LoadCustomerOptionsAsync(ICustomerService customerService)
     * {
     * var customers = await customerService.GetAllCustomersAsync();
     * CustomerOptions = new ObservableCollection<Customer>(customers);
     * */

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TicketPriority SelectedPriority { get; set; }
    [ObservableProperty]
    public partial string SuccessMessage { get; set; } = string.Empty;

    /*[ObservableProperty]
    public partial ObservableCollection<TicketPriority> PriorityOptions { get; } = new(Enum.GetValues<TicketPriority>()); */
    public ObservableCollection<TicketPriority> PriorityOptions { get; } = new(Enum.GetValues<TicketPriority>());


    [RelayCommand]
    private void CreateTicket()
    {
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(SelectedCustomerName))
        {
            ErrorMessage = "Vänligen välj en kund.";
            return;
        }
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = "Fyll i både rubrik och beskrivning.";
            return;
        }
        var newTicket = new SupportTicket(Guid.NewGuid(), Title, Description, SelectedPriority);
        //var ticket = new SupportTicket(SelectedCustomer.Id, Title, Description, SelectedPriority);
        // Logic to save the new ticket (e.g., call a service or repository)

        Title = string.Empty;
        Description = string.Empty;
        SelectedCustomerName = string.Empty;

        SuccessMessage = "Ärendet har skapats.";
    }

    private Guid _ticketId;
}
