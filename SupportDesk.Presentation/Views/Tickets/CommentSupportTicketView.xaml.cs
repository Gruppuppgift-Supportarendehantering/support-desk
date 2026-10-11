using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.ViewModels.Tickets;

namespace SupportDesk.Presentation.Views.Tickets;

public sealed partial class CommentSupportTicketView : Page
{
    public CommentSupportTicketViewModel ViewModel { get; }

    public CommentSupportTicketView()
    {
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetRequiredService<CommentSupportTicketViewModel>();
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
}
