using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.Helpers;
using SupportDesk.Presentation.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class EditSupportTicketViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private readonly INavigationService _navigationService;
    private readonly ISupportTicketService _supportTicketService;

    private SupportTicket? _ticket;

    public EditSupportTicketViewModel(
        ICustomerService customerService,
        INavigationService navigationService,
        ISupportTicketService supportTicketService)
    {
        _customerService = customerService;
        _navigationService = navigationService;
        _supportTicketService = supportTicketService;
    }

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TicketPriority Priority { get; set; }

    [ObservableProperty]
    public partial TicketStatus Status { get; set; }

    [ObservableProperty]
    public partial string NewCommentText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial ObservableCollection<TicketComment> Comments { get; set; } = new();

    [ObservableProperty]
    public partial string AssignedTechnician { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SuccessMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string OriginalTitle { get; set; } = string.Empty;

    public SupportTicket? CurrentTicket => _ticket;

    public ObservableCollection<TicketPriority> PriorityOptions { get; } = new(Enum.GetValues<TicketPriority>());
    public ObservableCollection<TicketStatus> StatusOptions { get; } = new(Enum.GetValues<TicketStatus>());
    public ObservableCollection<string> TechnicianOptions { get; } = new()
    {
        "Handläggare A",
        "Handläggare B",
        "Handläggare C"
    };

    public async Task LoadAsync(SupportTicket ticket)
    {
        _ticket = ticket;

        var response = await _customerService.GetCustomerByIdAsync(ticket.CustomerId);

        CustomerName = response.Customer?.CustomerName ?? "Kund saknas";
        OriginalTitle = ticket.Title;
        Title = ticket.Title;
        Description = ticket.Description;
        Priority = ticket.Priority;
        Status = ticket.Status;
        AssignedTechnician = ticket.AssignedTechnician;
        Comments = new ObservableCollection<TicketComment>(ticket.Comments);
    }

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Title))
        {
            ErrorMessage = "Fyll i en rubrik.";
            return;
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = "Fyll i en beskrivning.";
            return;
        }
        if (_ticket is null)
        {
            ErrorMessage = "Inget ärende är öppnat.";
            return;
        }

        _ticket.UpdateDetails(Title, Description);
        _ticket.ChangePriority(Priority);
        _ticket.ChangeStatus(Status);

        if (!string.IsNullOrWhiteSpace(AssignedTechnician))
        {
            _ticket.AssignTechnician(AssignedTechnician);
        }

        var result = await _supportTicketService.UpdateTicketAsync(_ticket);
        if (!result.Succeeded)
        {
            ErrorMessage = result.ErrorMessage ?? "Kunde inte spara ändringarna.";
            return;
        }

        SuccessMessage = "Ändringarna har sparats.";
    }

    [RelayCommand]
    private void AddComment()
    {
        if (string.IsNullOrWhiteSpace(NewCommentText))
        {
            return;
        }
        Comments.Add(new TicketComment(NewCommentText));
        NewCommentText = string.Empty;
    }

    [RelayCommand]
    public void GoBack()
    {
        _navigationService.Navigate(AppPage.SupportTicketOverview);
    }
}