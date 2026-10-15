using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupportDesk.Infrastructure.Tickets;

public class SupportTicketRepository : ISupportTicketRepository
{


    private readonly string _filePath;

    public SupportTicketRepository() : this(GetDefaultFilePath()) { }

    public SupportTicketRepository(string filePath)
    {
        _filePath = filePath;
        Debug.WriteLine(_filePath);
        
    }

    private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    public static string GetDefaultFilePath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SupportDesk", "supporttickets.json");
    }

    public async Task AddTicketToJsonAsync(SupportTicket ticket)
    {
        //_ticketList.Add(ticket);

        List<SupportTicket> tickets = await ReadTicketsAsync();
        // felsök efter duplicering tickets
        if (tickets.Any(t => t.Id == ticket.Id))
        {
            throw new InvalidOperationException("Ärendet finns redan");
        }

        tickets.Add(ticket);
        await SaveToJsonAsync(tickets);
    }

    public async Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync()
    {

        var tickets = await ReadTicketsAsync();
        return tickets;
    }

    public async Task<SupportTicket?> GetTicketbyIdAsync(Guid ticketId)
    {
        List<SupportTicket> tickets = await ReadTicketsAsync();

        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException("Supportärendenummer får inte vara tomt", nameof(ticketId));
        }

        return tickets.FirstOrDefault(x => x.Id == ticketId);
    }

    public async Task SaveToJsonAsync(List<SupportTicket> tickets)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        string json = JsonSerializer.Serialize(tickets, jsonOptions);

        string tempPath = _filePath + ".tmp";

        await File.WriteAllTextAsync(tempPath, json);
        File.Move(tempPath, _filePath, overwrite: true);

    }

    public async Task<List<SupportTicket>> ReadTicketsAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<SupportTicket>();
        }

        string json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<SupportTicket>>(json, jsonOptions)
            ?? throw new JsonException("Ticket file does not contain a valid ticket list.");
    }

    public async Task UpdateTicketAsync(SupportTicket ticket)
    {
        var tickets = await ReadTicketsAsync();

        var existingTicket = tickets.FirstOrDefault(t => t.Id == ticket.Id);

        if (existingTicket is null)
        {
            throw new KeyNotFoundException($"Ärendet med ID {ticket.Id} hittades inte.");
        }

        var updatedTicket = tickets.Select(t => t.Id == ticket.Id ? ticket : t).ToList();

        await SaveToJsonAsync(updatedTicket);
    }

    public Task DeleteTicketAsync(Guid ticketId)
    {
        throw new NotImplementedException();
    }
}
