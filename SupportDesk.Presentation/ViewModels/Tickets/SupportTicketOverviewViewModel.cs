using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Domain.Tickets;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;
using SupportDesk.Application.Tickets;
using System.Threading.Tasks;
using System.Linq;

namespace SupportDesk.Presentation.ViewModels.Tickets
{
    public partial class SupportTicketOverviewViewModel : ObservableObject
    {
        private readonly ISupportTicketService _supportTicketService;

        public SupportTicketOverviewViewModel(ISupportTicketService supportTicketService)
        {
            _supportTicketService = supportTicketService;
        }

        [ObservableProperty]
        public partial ObservableCollection<SupportTicket> Tickets { get; set; } = new();

        [ObservableProperty]
        public partial SupportTicket? SelectedTicket { get; set; }

        public int NewCount => Tickets.Count(t => t.Status == TicketStatus.New);

        public int InProgressCount => Tickets.Count(t => t.Status == TicketStatus.InProgress);

        public int ResolvedCount => Tickets.Count(t => t.Status == TicketStatus.Resolved);

        [RelayCommand]
        private async Task LoadTicketsAsync()
        {
            var tickets = await _supportTicketService.GetAllTicketsAsync();
            Tickets.Clear();
            foreach (var ticket in tickets.OrderByDescending(t => t.CreatedAt))
            {
                Tickets.Add(ticket);
            }

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
            // Logic to open the selected ticket, e.g., navigate to a detailed view
        }
       /* public SupportTicketOverviewViewModel()
        {
            // Sample data for demonstration purposes
            /* Tickets.Add(new SupportTicket(Guid.NewGuid(), "Inloggning fungerar inte", "Kunden kan inte logga in efter uppdatering.", TicketPriority.High));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Skrivarstrul", "Skrivaren på våning 3 svarar inte.", TicketPriority.Low));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Skärmen Trasig", "Skärmen vill inte starta.", TicketPriority.Low));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
             Tickets.Add(new SupportTicket(Guid.NewGuid(), "Internet nere", "Internet fungerar ej på maskinen.", TicketPriority.Medium));
         }*/

        }
    }
