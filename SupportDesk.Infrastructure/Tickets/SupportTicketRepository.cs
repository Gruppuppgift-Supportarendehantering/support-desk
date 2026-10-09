using SupportDesk.Application.Dtos.Tickets;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupportDesk.Infrastructure.Tickets;

public class SupportTicketRepository : ISupportTicketRepository
{
    private readonly List<SupportTicket> _ticketList = new List<SupportTicket>();

    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SupportDesk", "supporttickets.json");

    public SupportTicketRepository()
    {
        Debug.WriteLine(_filePath); //ta bort sen
        //LoadFromJson();
    }

    private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    

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

    public async Task<IEnumerable<SupportTicket>> GetAllTicketsAsync()
    {
        
        var tickets = await ReadTicketsAsync();
        return tickets;
    }

    public async Task<SupportTicket?> GetTicketbyIdAsync(Guid ticketId)
    {
        List<SupportTicket> tickets = await ReadTicketsAsync();

        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException("Supportärendenummer får inte vara tomt",nameof(ticketId));
        }
        
        return tickets.FirstOrDefault(x => x.Id == ticketId);
    }

    public async Task SaveToJsonAsync(List<SupportTicket> tickets)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        string json = JsonSerializer.Serialize(tickets, jsonOptions);

        await File.WriteAllTextAsync(_filePath, json);

        //tempPath = _filePath + ".tmp";
        //File.WriteAllText(tempPath, json); skydd för filkorruption vid skrivning, men inte implementerat än

    }

    public async Task<List<SupportTicket>> ReadTicketsAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<SupportTicket>();
        }

        string json = await File.ReadAllTextAsync(_filePath);

        List<SupportTicket>? tickets = JsonSerializer.Deserialize<List<SupportTicket>>(json, jsonOptions);

        if (tickets is null)
        {
            return new List<SupportTicket>();
        }

        return tickets;
        
    }

    public Task UpdateTicketAsync(SupportTicket ticket)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTicketAsync(Guid ticketId)
    {
        throw new NotImplementedException();
    }
}
