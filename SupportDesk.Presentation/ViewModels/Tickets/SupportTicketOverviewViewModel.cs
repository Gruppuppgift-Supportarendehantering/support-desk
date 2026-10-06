using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Domain.Tickets;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Presentation.ViewModels.Tickets
{
    public partial class SupportTicketOverviewViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<SupportTicket> Tickets { get; set; } = new();

        [ObservableProperty]
        public partial SupportTicket? SelectedTicket { get; set; }


        [RelayCommand]
        private void OpenTicket()
        {
            if (SelectedTicket == null)
            {
                return;
            }
            // Logic to open the selected ticket, e.g., navigate to a detailed view
        }
        public SupportTicketOverviewViewModel()
        {
            // Sample data for demonstration purposes
            Tickets.Add(new SupportTicket(Guid.NewGuid(), "Inloggning fungerar inte", "Kunden kan inte logga in efter uppdatering.", TicketPriority.High));
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
        }
    }
}
