using SupportDesk.Domain.Customers;
using SupportDesk.Domain.Tickets;
using SupportDesk.Domain.ValueObjects;
using SupportDesk.Infrastructure.Customers;
using SupportDesk.Infrastructure.Tickets;
using System.Text.Json;
namespace SupportDesk.Tests.Storage;

public class JsonStorageIntegrationTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "SupportDeskTests", Guid.NewGuid().ToString());
    private readonly string _customersTestFilePath;
    private readonly string _ticketsTestFilePath;

    public JsonStorageIntegrationTests()
    {
        Directory.CreateDirectory(_directory);
        _customersTestFilePath = Path.Combine(_directory, "customers.json");
        _ticketsTestFilePath = Path.Combine(_directory, "supporttickets.json");
    }

    public void Dispose()
    {
        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, recursive: true);
        }
    }

    [Fact]
    public async Task SavedCustomerAndTicket_AreRestoredByNewRepositoryInstances()
    {
        // Arrange
        var customer = new Customer(Guid.NewGuid(), new Name("Kalle Kula"),
            new EmailAddress("kalle@example.com"), isCompany: false);

        var ticket = new SupportTicket(customer.CustomerId, "Kan inte logga in",
            "Felmeddelande vid inloggning", TicketPriority.High);
        ticket.AssignTechnician("Anna Andersson");
        ticket.ChangeStatus(TicketStatus.InProgress);
        ticket.AddComment("Felsökning har påbörjats.");

        var customerWriter = new JsonFileCustomerRepository(_customersTestFilePath);
        var ticketWriter = new SupportTicketRepository(_ticketsTestFilePath);

        // Act: spara med en uppsättning repositories ...
        Assert.True(await customerWriter.CreateAsync(customer));
        await ticketWriter.AddTicketToJsonAsync(ticket);

        // ... och läs med helt nya instanser mot samma filer
        var customerReader = new JsonFileCustomerRepository(_customersTestFilePath);
        var ticketReader = new SupportTicketRepository(_ticketsTestFilePath);

        var loadedCustomer = await customerReader.GetByIdAsync(customer.CustomerId);
        var loadedTicket = await ticketReader.GetTicketbyIdAsync(ticket.Id);

        // Assert: kunden
        Assert.NotNull(loadedCustomer);
        Assert.Equal(customer.CustomerId, loadedCustomer.CustomerId);
        Assert.Equal("Kalle Kula", loadedCustomer.CustomerName.Value);
        Assert.Equal("kalle@example.com", loadedCustomer.EmailAddress.Value);

        // Assert: ärendet
        Assert.NotNull(loadedTicket);
        Assert.Equal(ticket.Id, loadedTicket.Id);
        Assert.Equal(ticket.CreatedAt, loadedTicket.CreatedAt);
        Assert.Equal("Kan inte logga in", loadedTicket.Title);
        Assert.Equal("Anna Andersson", loadedTicket.AssignedTechnician);
        Assert.Equal(TicketStatus.InProgress, loadedTicket.Status);

        // Assert: kommentaren
        Assert.Single(loadedTicket.Comments);
        Assert.Equal("Felsökning har påbörjats.", loadedTicket.Comments[0].Text);
        Assert.Equal(ticket.Comments[0].CreatedAt, loadedTicket.Comments[0].CreatedAt);

        // Assert: kundkopplingen
        Assert.Equal(loadedCustomer.CustomerId, loadedTicket.CustomerId);
    }

    [Theory]
    [InlineData("{ detta är inte giltig json [")]
    [InlineData("null")]
    [InlineData("")]
    public async Task CorruptTicketFile_ThrowsJsonException_AndIsNotOverwritten(string fileContent)
    {
        // Arrange
        await File.WriteAllTextAsync(_ticketsTestFilePath, fileContent);
        var repository = new SupportTicketRepository(_ticketsTestFilePath);

        // Act + Assert: läsning rapporterar felet
        await Assert.ThrowsAnyAsync<JsonException>(() => repository.GetAllTicketsAsync());

        // Även en skrivande operation (läs-ändra-skriv) måste avbrytas
        var newTicket = new SupportTicket(Guid.NewGuid(), "Titel", "Beskrivning", TicketPriority.Low);
        await Assert.ThrowsAnyAsync<JsonException>(() => repository.AddTicketToJsonAsync(newTicket));

        // Filens innehåll är orört, och ingen temp-fil har lämnats kvar
        Assert.Equal(fileContent, await File.ReadAllTextAsync(_ticketsTestFilePath));
        Assert.False(File.Exists(_ticketsTestFilePath + ".tmp"));
    }

    [Theory]
    [InlineData("{ detta är inte giltig json [")]
    [InlineData("null")]
    [InlineData("")]
    public async Task CorruptCustomerFile_ThrowsJsonException_AndIsNotOverwritten(string fileContent)
    {
        // Arrange
        await File.WriteAllTextAsync(_customersTestFilePath, fileContent);
        var repository = new JsonFileCustomerRepository(_customersTestFilePath);

        // Act + Assert
        await Assert.ThrowsAnyAsync<JsonException>(() => repository.GetAllAsync());

        var customer = new Customer(Guid.NewGuid(), new Name("Kalle Kula"),
            new EmailAddress("kalle@example.com"), isCompany: false);
        await Assert.ThrowsAnyAsync<JsonException>(() => repository.CreateAsync(customer));

        Assert.Equal(fileContent, await File.ReadAllTextAsync(_customersTestFilePath));
        Assert.False(File.Exists(_customersTestFilePath + ".tmp"));
    }
}
