using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Presentation.Navigation;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Customers;

public partial class EditCustomerViewModel(
    ICustomerService customerService,
    INavigationService navigationService) : ObservableObject
{
    private readonly ICustomerService _customerService = customerService;
    private readonly INavigationService _navigationService = navigationService;
    public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

    private Guid _customerId;

    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string EmailAddress { get; set; } = string.Empty;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasMessage))]
    public partial string Message { get; set; } = string.Empty;

    public async Task LoadCustomerAsync(Guid customerId)
    {
        _customerId = Guid.Empty;
        CustomerName = string.Empty;
        EmailAddress = string.Empty;
        Message = string.Empty;

        try
        {
            var response = await _customerService.GetCustomerByIdAsync(customerId);

            if (!response.Succeeded || response.Customer is null)
            {
                Message = response.ErrorMessage ?? "Kunden kunde inte hämtas.";

                return;
            }

            _customerId = response.Customer.CustomerId;
            CustomerName = response.Customer.CustomerName;
            EmailAddress = response.Customer.EmailAddress;

        }
        catch (IOException)
        {
            Message = "Kundfilen kunde inte läsas.";
        }
        catch (UnauthorizedAccessException)
        {
            Message = "Applikationen saknar behörighet till kundfilen.";
        }
        catch (JsonException)
        {
            Message = "Kundfilen innehåller ogiltig data.";
        }
        catch (ArgumentException)
        {
            Message = "Kunden har ogiltigt namn eller e-postadress.";
        }
        
    }
    [RelayCommand]
    private async Task UpdateAsync()
    {
        Message = string.Empty;

        if (_customerId == Guid.Empty)
        {
            Message = "Ingen kund har hämtats.";
            return;
        }

        var request = new UpdateCustomerRequest(_customerId, CustomerName, EmailAddress);

        try
        {
            var response = await _customerService.UpdateCustomerAsync(request);

            if (!response.Succeeded || response.Customer is null)
            {
                Message = response.ErrorMessage
                    ?? "Kunden kunde inte uppdateras.";
                return;
            }

            GoBack();
        }
        catch (IOException)
        {
            Message = "Kundfilen kunde inte läsas eller sparas.";
        }
        catch (UnauthorizedAccessException)
        {
            Message = "Applikationen saknar behörighet till kundfilen.";
        }
        catch (JsonException)
        {
            Message = "Kundfilen innehåller ogiltig data.";
        }
        catch (ArgumentException)
        {
            Message = "En kund i filen har ogiltiga uppgifter.";
        }
        
    }
    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
