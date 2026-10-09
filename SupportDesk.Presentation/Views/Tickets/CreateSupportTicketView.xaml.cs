using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Tickets;

namespace SupportDesk.Presentation.Views.Tickets;
public sealed partial class CreateSupportTicketView : Page
{
    public CreateSupportTicketViewModel ViewModel { get; }

    public CreateSupportTicketView()
    {

        //tillfällig lösning, gör DI manuellt
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetRequiredService<CreateSupportTicketViewModel>();

        InitializeComponent();

        
        
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }

}
