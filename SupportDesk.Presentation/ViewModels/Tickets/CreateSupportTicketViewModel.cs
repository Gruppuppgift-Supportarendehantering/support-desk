using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Customers;
using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.Navigation;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class CreateSupportTicketViewModel : ObservableObject
{
    private readonly ISupportTicketService _supportTicketService;
    private readonly ICustomerService _customerService;
    private readonly INavigationService _navigationService;

    public CreateSupportTicketViewModel(ICustomerService customerService, ISupportTicketService supportTicketService, INavigationService navigationService)
    {
        _supportTicketService = supportTicketService;
        _customerService = customerService;
        _navigationService = navigationService;

        _ = LoadCustomersAsync();
    }

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SuccessMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial CustomerData? SelectedCustomer { get; set; }

    [ObservableProperty]
    public partial TicketPriority TicketPriority { get; set; }

    public ObservableCollection<CustomerData> Customers { get; } = [];

    public TicketPriority[] Priorities { get; } = Enum.GetValues<TicketPriority>();

    [RelayCommand]
    private async Task UICreateTicketAsync()
    {
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Title))
        {
            ErrorMessage = "Titel får inte vara tom.";
            return;
        }
        if (SelectedCustomer == null)
        {
            ErrorMessage = "Välj en kund först.";
            return;
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = "Beskrivning får inte vara tom.";
            return;
        }
        var request = new SupportTicketRequest(Title, Description, SelectedCustomer.CustomerId, TicketPriority);

        SupportTicketResult result;

        try
        {
            result = await _supportTicketService.CreateTicketAsync(request);
        }
        catch (IOException)
        {
            ErrorMessage = "Ärendefilen kunde inte läsas eller sparas.";
            return;
        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessage = "Applikationen saknar behörighet till ärendefilen";
            return;
        }
        catch (JsonException)
        {
            ErrorMessage = "Ärendefilen innehåller ogiltig data. Ärendet sparas inte.";
            return;
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
            return;
        }
        catch (InvalidOperationException)
        {
            ErrorMessage = "Ärendet finns redan.";
            return;
        }


        if (!result.Succeeded)
        {
            await LoadCustomersAsync();
            SelectedCustomer = null;

            ErrorMessage = result.ErrorMessage ?? "Ärendet kunde inte skapas.";
            return;
        }

        SuccessMessage = $"Ärendet har skapats!";

        Title = string.Empty;
        Description = string.Empty;
        SelectedCustomer = null;
        TicketPriority = TicketPriority.Medium;
    }


    public async Task LoadCustomersAsync()
    {
        try
        {
            var response = await _customerService.GetAllCustomersAsync();

            if (!response.Succeeded)
            {
                ErrorMessage = response.ErrorMessage ?? "Kunde inte hämta kunder.";
                return;
            }

            Customers.Clear();

            foreach (var customer in response.Customers)
            {
                Customers.Add(customer);
            }
        }
        catch (IOException)
        {
            ErrorMessage = "Kundfilen kunde inte läsas in";
        }
        catch(UnauthorizedAccessException)
        {
            ErrorMessage = "Applikationen saknar behörighet till kundfilen.";
        }
        catch (JsonException)
        {
            ErrorMessage = "Kundfilen innehåller ogiltig data.";
        }
        catch (ArgumentException)
        {
            ErrorMessage = "En kund i filen har ogiltigt namn eller e-postadress.";
        }
    }

    [RelayCommand]
    public void GoBack()
    {
        _navigationService.Navigate(AppPage.SupportTicketOverview);
    }
}