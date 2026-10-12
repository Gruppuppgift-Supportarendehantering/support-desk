using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Domain.Tickets;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using SupportDesk.Application.Tickets;
using System.Threading.Tasks;
using System.Linq;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Dtos.Customers;

namespace SupportDesk.Presentation.ViewModels.Tickets;

public partial class SupportTicketOverviewViewModel : ObservableObject
{
    private readonly ISupportTicketService _supportTicketService;
    private readonly ICustomerService _customerService;

    private List<SupportTicket> _allTickets = [];
    private List<CustomerData> _customers = [];
    public string[] StatusFilters { get; } = ["Alla", "Nytt", "Pågående", "Löst"];
    public SupportTicketOverviewViewModel(
        ISupportTicketService supportTicketService,
        ICustomerService customerService)
    {
        _supportTicketService = supportTicketService;
        _customerService = customerService;
    }

    [ObservableProperty]
    public partial ObservableCollection<SupportTicket> Tickets { get; set; } = new();

    [ObservableProperty]
    public partial SupportTicket? SelectedTicket { get; set; }

    [ObservableProperty]
    public partial string SelectedStatusFilter { get; set; } = "Alla";

    partial void OnSelectedStatusFilterChanged(string value)
    {
        ApplyFilters();
    }

    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var query = SearchText.Trim();

        TicketStatus? status = SelectedStatusFilter switch
        {
            "Nytt" => TicketStatus.New,
            "Pågående" => TicketStatus.InProgress,
            "Löst" => TicketStatus.Resolved,
            _ => null
        };

        var filtered = _allTickets
            .Where(t => t.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
            || _customers.Any(c => c.CustomerId == t.CustomerId && c.CustomerName.Contains(query, StringComparison.OrdinalIgnoreCase)))
            .Where(t => status == null || t.Status == status)
            .OrderByDescending(t => t.CreatedAt);

        SelectedTicket = null;
        Tickets.Clear();

        foreach (var ticket in filtered)
        {
            Tickets.Add(ticket);
        }
    }

    public int NewCount => _allTickets.Count(t => t.Status == TicketStatus.New);

    public int InProgressCount => _allTickets.Count(t => t.Status == TicketStatus.InProgress);

    public int ResolvedCount => _allTickets.Count(t => t.Status == TicketStatus.Resolved);

    [RelayCommand]
    private async Task LoadTicketsAsync()
    {
        var tickets = await _supportTicketService.GetAllTicketsAsync();
        var customerResponse = await _customerService.GetAllCustomersAsync();

        _allTickets = tickets.ToList();

        _customers = customerResponse.Succeeded
            ? customerResponse.Customers.ToList()
            : [];
            

        ApplyFilters();

        OnPropertyChanged(nameof(NewCount));
        OnPropertyChanged(nameof(InProgressCount));
        OnPropertyChanged(nameof(ResolvedCount));
    }

    [RelayCommand]
    private void OpenTicket()
    {
        if (SelectedTicket == null)
        {
            return;
        }

    }
  

}

