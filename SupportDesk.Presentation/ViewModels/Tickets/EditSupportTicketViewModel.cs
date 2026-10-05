using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using SupportDesk.Domain.Tickets;
using System;
using CommunityToolkit.Mvvm.Input;
namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class EditSupportTicketViewModel : ObservableObject
{
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

    public ObservableCollection<TicketPriority> PriorityOptions { get; } = new(Enum.GetValues<TicketPriority>());
    public ObservableCollection<TicketStatus> StatusOptions { get; } = new(Enum.GetValues<TicketStatus>());
    public ObservableCollection<string> TechnicianOptions { get; } = new()
    {       
        "Technician A",
        "Technician B",
        "Technician C"
    };

    [RelayCommand]
    private void SaveChanges()
    {
       
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

    private Guid _ticketId;

}