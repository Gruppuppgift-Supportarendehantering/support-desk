using SupportDesk.Domain.Tickets;
using SupportDesk.Presentation.Helpers;

namespace SupportDesk.Tests.Tickets;

public class TicketFilteringTests
{
    [Theory]
    [InlineData("hårddisk")]
    [InlineData("HÅRDDISK")]
    [InlineData("HåRdDiSK")]
    public void Search_Should_ReturnMatchingTicketsRegardlessOfCase(string searchText)
    {
        var firstTicket = new SupportTicket(
            Guid.NewGuid(),
            "Extern hårddisk fungerar inte",
            "Hårdisken startar med avslutar med detsamma",
            TicketPriority.Medium);

        var secondTicket = new SupportTicket(
            Guid.NewGuid(),
            "SSD-hårddisk konstigt ljud",
            "Hårddisken gör ett konstigt ljud vid skrivning till den.",
            TicketPriority.Low);

        var thirdTicket = new SupportTicket(
            Guid.NewGuid(),
            "Trasig kontorsstol",
            "Går inte att höja och sänka",
            TicketPriority.High);

        var tickets = new[] { firstTicket, secondTicket, thirdTicket };

        var result = SupportTicketSearch.Search(tickets, searchText);

        Assert.Equal(2, result.Count);
        Assert.Contains(firstTicket, result);
        Assert.Contains(secondTicket, result);
        Assert.DoesNotContain(thirdTicket, result);
    }

    [Theory]
    [InlineData(TicketStatus.New)]
    [InlineData(TicketStatus.InProgress)]
    [InlineData(TicketStatus.Resolved)]
    public void FilterByStatus_Should_ReturnOnlyTicketsWithSelectedStatus(TicketStatus status)
    {
        var firstTicket = new SupportTicket(
            Guid.NewGuid(),
            "Problem med dator",
            "Kan inte starta",
            TicketPriority.Low);

        var secondTicket = new SupportTicket(
            Guid.NewGuid(),
            "Problem med tangentbord",
            "Tangentbord kopplar ifrån",
            TicketPriority.High);

        firstTicket.ChangeStatus(status);
        secondTicket.ChangeStatus(status);

        var thirdTicket = new SupportTicket(
            Guid.NewGuid(),
            "Trasig skärm",
            "Skärmen har ett antal döda pixlar",
            TicketPriority.Medium);

        TicketStatus otherStatus;

        if (status == TicketStatus.New)
        {
            otherStatus = TicketStatus.Resolved;
        }
        else
        {
            otherStatus = TicketStatus.New;
        }

        thirdTicket.ChangeStatus(otherStatus);

        var tickets = new[] { firstTicket, thirdTicket, secondTicket };

        var result = SupportTicketFilter.FilterByStatusAndPriority(
            tickets,
            status: status);

        Assert.Equal(2, result.Count);
        Assert.Contains(firstTicket, result);
        Assert.Contains(secondTicket, result);
        Assert.DoesNotContain(thirdTicket, result);
        Assert.All(result, ticket => Assert.Equal(status, ticket.Status));
    }
}
