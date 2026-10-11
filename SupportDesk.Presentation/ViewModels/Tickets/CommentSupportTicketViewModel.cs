using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class CommentSupportTicketViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public CommentSupportTicketViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [ObservableProperty]
    public partial string CommentText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SuccessMessage { get; set; } = string.Empty;

    private SupportTicket? _ticket;

    public void Load(SupportTicket ticket)
    {
        _ticket = ticket;
    }

    [RelayCommand]
    private void AddComment()
    {
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        if (_ticket is null)
        {
            ErrorMessage = "Inget ärende är öppnat.";
            return;
        }

        if (string.IsNullOrWhiteSpace(CommentText))
        {
            ErrorMessage = "Kommentaren kan inte vara tom.";
            return;
        }

        _ticket.AddComment(CommentText);
        SuccessMessage = "Kommentaren har lagts till.";
        CommentText = string.Empty;
    }
    [RelayCommand]
    public void GoBack()
    {
        _navigationService.Navigate(AppPage.SupportTicketOverview);
    }
}

