namespace SupportDesk.Domain.Tickets;

public class SupportTicket
{
    private readonly List<TicketComment> _comments = new();

    public Guid Id { get; } //supportärendet
    public Guid CustomerId { get; } //kundens id
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TicketPriority Priority { get; private set; }
    public TicketStatus Status { get; private set; }
    public string AssignedTechnician { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; }

    public IReadOnlyList<TicketComment> Comments => _comments.AsReadOnly();

    public SupportTicket(Guid customerId, string title, string description, TicketPriority priority)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentException("Kundens ID kan inte vara tomt.", nameof(customerId));
        }

        ValidateDetails(title, description);

        Id = Guid.NewGuid();
        CustomerId = customerId;
        Title = title.Trim();
        Description = description.Trim();
        Priority = priority;
        Status = TicketStatus.New;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string title, string description)
    {
        ValidateDetails(title, description);
        Title = title.Trim();
        Description = description.Trim();
    }

    public void AssignTechnician(string technicianName)
    {
        if (string.IsNullOrWhiteSpace(technicianName))
        {
            throw new ArgumentException("Technician name cannot be empty.", nameof(technicianName));
        }
        AssignedTechnician = technicianName;
    }

    public void ChangePriority(TicketPriority priority)
    {
        Priority = priority;
    }

    public void ChangeStatus(TicketStatus status)
    {
        Status = status;
    }

    public void AddComment(string commentText)
    {
        var comment = new TicketComment(commentText);
        _comments.Add(comment);
    }

    private static void ValidateDetails(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Titel kan inte vara tom.", nameof(title));
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Beskrivning kan inte vara tom.", nameof(description));
        }
    }

}
