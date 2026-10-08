using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Tickets;
using System.Collections.ObjectModel;

namespace SupportDesk.Presentation.Views.Tickets;
public sealed partial class CreateSupportTicketView : Page
{
    public CreateSupportTicketViewModel ViewModel { get; }

    public CreateSupportTicketView(/*CreateSupportTicketViewModel viewModel*/)
    {
        InitializeComponent();

        //ViewModel = viewModel;
        //tillfällig lösning, gör DI manuellt
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetRequiredService<CreateSupportTicketViewModel>();
    }

    

}
