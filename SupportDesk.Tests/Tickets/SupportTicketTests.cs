
using SupportDesk.Domain.Tickets;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupportDesk.Tests.Tickets;

public class SupportTicketTests
{
    [Fact]
    public void CreateTicket_WithValidDetails_SetsStatusToNew()
    {
        Guid customerId = Guid.NewGuid();

        var ticket = new SupportTicket(customerId, "Kan inte logga in", "Jag har problem att logga in på mitt konto", TicketPriority.Medium);


        Assert.NotEqual(Guid.Empty, ticket.Id);
        Assert.Equal(customerId, ticket.CustomerId);
        Assert.Equal("Kan inte logga in", ticket.Title);
        Assert.Equal(TicketStatus.New, ticket.Status);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateTicket_WithEmptyTitle_ThrowsArgumentException(string title)
    {
        Guid customerId = Guid.NewGuid();

        Assert.Throws<ArgumentException>(() => new SupportTicket(customerId, title, "Beskrivning av problemet.", TicketPriority.Medium));
    }

    [Fact]
    public void JsonRoundTrip_PreservesTicketIdAndCreatedAt()
    {
        var ticket = new SupportTicket(Guid.NewGuid(), "Datorproblem", "Datorn startar inte", TicketPriority.Low);

        ticket.AssignTechnician("Anna Andersson");
        ticket.ChangeStatus(TicketStatus.InProgress);
        ticket.AddComment("Felsökning har påbörjats.");

        var originalId = ticket.Id;
        var originalCreatedAt = ticket.CreatedAt;
        var originalCommentTime = ticket.Comments[0].CreatedAt;

        var jsonoptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };

        var json = JsonSerializer.Serialize(ticket, jsonoptions);
        Console.WriteLine(json);

        var restored = JsonSerializer.Deserialize<SupportTicket>(json, jsonoptions);

        Assert.NotNull(restored);
        Assert.Equal(originalId, restored.Id);
        Assert.Equal(originalCreatedAt, restored.CreatedAt);
        Assert.Equal(TicketStatus.InProgress, restored.Status);
        Assert.Equal("Anna Andersson", restored.AssignedTechnician);
        Assert.Single(restored.Comments);
        Assert.Equal(originalCommentTime, restored.Comments[0].CreatedAt);
    }

}
