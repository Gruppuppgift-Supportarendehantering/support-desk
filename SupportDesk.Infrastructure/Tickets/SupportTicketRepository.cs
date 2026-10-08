using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupportDesk.Infrastructure.Tickets;

public class SupportTicketRepository : ISupportTicketRepository
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "supporttickets.json");

    private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    public SupportTicketRepository()
    {
        Debug.WriteLine(_filePath);
    }

    private async Task<List<SupportTicket>> ReadTicketsAsync()
    {
        if (!File.Exists(_filePath)) return [];

        string json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<SupportTicket>>(json, jsonOptions) ?? [];
    }

    private async Task SaveTicketsAsync(List<SupportTicket> tickets)
    {
        string json = JsonSerializer.Serialize(tickets, jsonOptions);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task AddAsync(SupportTicket ticket)
    {
        var tickets = await ReadTicketsAsync();
        tickets.Add(ticket);
        await SaveTicketsAsync(tickets);
    }

    public async Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync()
    {
        return await ReadTicketsAsync();
    }

    public async Task<SupportTicket?> GetByIdAsync(Guid ticketId)
    {
        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException("Supportärendenummer får inte vara tomt", nameof(ticketId));
        }

        var tickets = await ReadTicketsAsync();
        return tickets.FirstOrDefault(x => x.Id == ticketId);
    }
}
