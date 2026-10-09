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

public partial class CreateCustomerViewModel(
    ICustomerService customerService,
    INavigationService navigationService) : ObservableObject
{
    private readonly ICustomerService _customerService = customerService;
    private readonly INavigationService _navigationService = navigationService;
    public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string EmailAddress { get; set; } = string.Empty;
    [ObservableProperty]
    public partial bool IsCompany { get; set; }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasMessage))]
    public partial string Message { get; set; } = string.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        Message = string.Empty;

        var request = new CreateCustomerRequest(
            CustomerName,
            EmailAddress,
            IsCompany);

        try
        {
            var response = await _customerService.CreateCustomerAsync(request);

            if (!response.Succeeded)
            {
                Message = response.ErrorMessage ?? "Kunden kunde inte sparas.";
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
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
