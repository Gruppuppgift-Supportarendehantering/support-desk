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



}
