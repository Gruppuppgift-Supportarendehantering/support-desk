
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Customers;
using SupportDesk.Domain.Tickets;
using System;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Tickets;


//instansiera navigation
public partial class CreateSupportTicketViewModel : ObservableObject
{
    private readonly ISupportTicketService _supportTicketService;

    public CreateSupportTicketViewModel(ISupportTicketService supportTicketService)
    {
        _supportTicketService = supportTicketService;
    }
    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SuccessMessage {  get; set; } = string.Empty;

    [ObservableProperty] 
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TemporaryCustomer? SelectedCustomer {  get; set; }

    [ObservableProperty]
    public partial Guid CustomerId { get; set; }

    [ObservableProperty]
    public partial TicketPriority TicketPriority { get; set; }


    [RelayCommand]
    private async Task UICreateTicketAsync()
    {
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

        SupportTicketResult result = await _supportTicketService.CreateTicketAsync(request);

        SuccessMessage = $"Ärendet har skapats! Med ärendenummer {result.TicketId}";

        Title = string.Empty;
        Description = string.Empty;
        SelectedCustomer = null;
        TicketPriority = TicketPriority.Medium;
    }

    //placeholder för customers

    public record TemporaryCustomer(string Name, Guid CustomerId);

    public TemporaryCustomer[] Customers { get; } =
    {
        new("Kendall Collins", Guid.NewGuid()),
        new("Henry Ross",Guid.NewGuid()),
        new("Vance DeLeon", Guid.NewGuid()),
        new("Victoria Burke", Guid.NewGuid()),
        new("Amber Rodriguez", Guid.NewGuid())
    };

    public TicketPriority[] Priorities { get; } = Enum.GetValues<TicketPriority>();
}
