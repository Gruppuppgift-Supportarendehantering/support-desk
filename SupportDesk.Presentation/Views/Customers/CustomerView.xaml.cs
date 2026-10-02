using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Customers;
using System;

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

    private async void DeleteCustomer_Click(object sender, RoutedEventArgs e)
    {
        var customer = ViewModel.SelectedCustomer;

        if (customer is null)
        {
            ViewModel.Message = "Välj en kund att ta bort.";
            return;
        }

        var dialog = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Title = "Ta bort kund",
            Content = $"Vill du ta bart {customer.CustomerName}?",
            PrimaryButtonText = "Ta bort",
            CloseButtonText = "Avbryt",
            DefaultButton = ContentDialogButton.Close
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            await ViewModel.DeleteCustomerCommand.ExecuteAsync(null);
        }
    }
}
