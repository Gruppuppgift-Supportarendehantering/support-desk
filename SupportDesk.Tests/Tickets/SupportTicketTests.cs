
using SupportDesk.Domain.Tickets;

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

}
