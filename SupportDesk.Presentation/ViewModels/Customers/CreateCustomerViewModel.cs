using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;

namespace SupportDesk.Presentation.ViewModels.Customers;

public partial class CreateCustomerViewModel(ICustomerService customerService) : ObservableObject
{
    private readonly ICustomerService _customerService = customerService;

    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string EmailAddress { get; set; } = string.Empty;
    [ObservableProperty]
    public partial bool IsCompany { get; set; }
    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;
}
