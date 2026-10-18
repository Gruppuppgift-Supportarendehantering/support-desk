using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.Navigation;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class CommentSupportTicketViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ISupportTicketService _supportTicketService;

    public CommentSupportTicketViewModel(INavigationService navigationService, ISupportTicketService supportTicketService)
    {
        _navigationService = navigationService;
        _supportTicketService = supportTicketService;
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
    private async Task AddCommentAsync()
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

        var result = await _supportTicketService.UpdateTicketAsync(_ticket);
        if (!result.Succeeded)
        {
            ErrorMessage = result.ErrorMessage ?? "Kunde inte spara kommentaren.";
            return;
        }
        SuccessMessage = "Kommentaren har lagts till.";
        CommentText = string.Empty;
    }
    [RelayCommand]
    public void GoBack()
    {
        _navigationService.Navigate(AppPage.SupportTicketOverview);
    }
}

