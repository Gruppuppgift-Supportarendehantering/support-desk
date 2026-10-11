using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Tickets;
using System;
using System.Collections.ObjectModel;
using Windows.Networking.BackgroundTransfer;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class EditSupportTicketViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;

    private SupportTicket? _ticket;

    public EditSupportTicketViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
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
        "Technician A",
        "Technician B",
        "Technician C"
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
    private void SaveChanges()
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
}