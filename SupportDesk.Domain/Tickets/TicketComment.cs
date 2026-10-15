using System.Text.Json.Serialization;

namespace SupportDesk.Domain.Tickets;

public class TicketComment
{
    public string Text { get; }
    public DateTime CreatedAt { get; }
    public TicketComment(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Kommentarstext kan inte vara tom.", nameof(text));
        }

        {
            Text = text;
            CreatedAt = DateTime.Now;
        }
    }

    [JsonConstructor]
    public TicketComment(string text, DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Kommentarstext kan inte vara tom.", nameof(text));
        }

        Text = text;
        CreatedAt = createdAt;
    }
}