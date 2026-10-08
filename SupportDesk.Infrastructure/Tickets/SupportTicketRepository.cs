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
        LoadFromJson();
    }

    private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    

    public async Task AddTicketToListAsync(SupportTicket ticket)
    {
        _ticketList.Add(ticket);
        await SaveToJsonAsync();
    }

    public async Task<IEnumerable<SupportTicket>> GetAllTicketsAsync()
    {
        //await Task.Yield() ?
        return _ticketList;
    }

    public async Task<SupportTicket?> GetTicketbyIdAsync(Guid ticketId)
    {
        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException("Supportärendenummer får inte vara tomt",nameof(ticketId));
        }
        
        return _ticketList.FirstOrDefault(x => x.Id == ticketId);
    }

    public async Task SaveToJsonAsync()
    {
        string json = JsonSerializer.Serialize(_ticketList, jsonOptions);

        File.WriteAllText(_filePath, json);

    }

    public void LoadFromJson()
    {
        if (!File.Exists(_filePath))
        {
            return;
        }

        string json = File.ReadAllText(_filePath);

        List<SupportTicket>? tickets = JsonSerializer.Deserialize<List<SupportTicket>>(json, jsonOptions);

        if (tickets is null)
        {
            return;
        }

        _ticketList.Clear();
        _ticketList.AddRange(tickets);
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
