using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Tickets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupportDesk.Infrastructure.Tickets;

public class SupportTicketRepository : ISupportTicketRepository
{
    private readonly List<SupportTicket> _ticketList = new List<SupportTicket>();

    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "supporttickets.json");
    private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    

    public void AddTicketToList(SupportTicket ticket)
    {
        _ticketList.Add(ticket);
    }

    public IEnumerable<SupportTicket> GetAllTickets()
    {
        return _ticketList;
    }

    public SupportTicket? GetbyId(Guid ticketId)
    {
        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException("Supportärendenummer får inte vara tomt",nameof(ticketId));
        }
        
        return _ticketList.FirstOrDefault(x => x.TicketId == ticketId);
    }

    public void SaveToJson()
    {
        string json = JsonSerializer.Serialize(_ticketList, jsonOptions);

        File.WriteAllText(_filePath, json);

    }
}
