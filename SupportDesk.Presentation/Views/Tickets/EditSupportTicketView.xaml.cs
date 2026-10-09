using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.ViewModels.Tickets;

namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class EditSupportTicketView : Page
{
    public EditSupportTicketViewModel ViewModel { get; } = new();

    public EditSupportTicketView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is SupportTicket ticket)
        {
            ViewModel.Load(ticket);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
        {
            Frame.GoBack();
            return;
        }

        Frame.Navigate(typeof(SupportTicketView));
    }

    private void AddCommentButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.CurrentTicket is SupportTicket ticket)
        {
            Frame.Navigate(typeof(CommentSupportTicketView), ticket);
        }
    }
}