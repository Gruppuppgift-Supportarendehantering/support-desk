using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Customers;

namespace SupportDesk.Presentation.Views.Customers;

public sealed partial class CustomerView : Page
{
    public CustomerViewModel ViewModel { get; }

    public CustomerView()
    {
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current)
            .Services.GetRequiredService<CustomerViewModel>();

        InitializeComponent();

        Loaded += CustomerView_Loaded;
    }

    private async void CustomerView_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadCustomerCommand.ExecuteAsync(null);
    }
}
