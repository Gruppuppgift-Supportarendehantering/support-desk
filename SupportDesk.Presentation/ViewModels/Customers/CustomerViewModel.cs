using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Presentation.Navigation;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Customers;

public partial class CustomerViewModel(
    ICustomerService customerService,
    INavigationService navigationService) : ObservableObject
{
    private readonly ICustomerService _customerService = customerService;
    private readonly INavigationService _navigationService = navigationService;

    public ObservableCollection<CustomerData> Customers { get; } = [];

    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;
    [ObservableProperty]
    public partial CustomerData? SelectedCustomer { get; set; }

    [RelayCommand]
    private async Task LoadCustomerAsync()
    {
        Message = string.Empty;

        try
        {
            var response = await _customerService.GetAllCustomersAsync();

            if (!response.Succeeded)
            {
                Message = response.ErrorMessage ?? "Kunderna kunde inte hämtas.";

                return;
            }

            Customers.Clear();

            foreach (var customer in response.Customers)
            {
                Customers.Add(customer);
            }

            if (Customers.Count == 0)
            {
                Message = "Det finns inga kunder sparade.";
            }
        }
        catch (IOException)
        {
            Message = "Kundfilen kunde inte läsas.";
        }
        catch (UnauthorizedAccessException)
        {
            Message = "Applikationen saknar behörighet till kundfilen.";
        }
        catch (ArgumentException)
        {
            Message = "En kund i filen har ogiltigt namn eller e-postadress.";
        }
    }

    [RelayCommand]
    private void CreateCustomer()
    {
        _navigationService.Navigate(AppPage.CreateCustomer);
    }
    [RelayCommand]
    private void EditCustomer()
    {
        if (SelectedCustomer is null)
        {
            Message = "Välj en kund att redigera.";
            return;
        }

        _navigationService.Navigate(AppPage.EditCustomer, SelectedCustomer.CustomerId.ToString());
    }
}
