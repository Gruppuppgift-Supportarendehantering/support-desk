using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SupportDesk.Presentation.ViewModels.Customers;
using System;

namespace SupportDesk.Presentation.Views.Customers;

public sealed partial class EditCustomerView : Page
{
    public EditCustomerViewModel ViewModel { get; }

    public EditCustomerView()
    {
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current)
            .Services.GetRequiredService<EditCustomerViewModel>();

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is not string id || 
            !Guid.TryParse(id, out var customerId) ||
            customerId == Guid.Empty)
        {
            ViewModel.Message = "Kund-ID saknas eller är ogiltigt.";
            return;
        }

        await ViewModel.LoadCustomerAsync(customerId);
    }
}
