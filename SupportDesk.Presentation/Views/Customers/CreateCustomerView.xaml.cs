using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels.Customers;

namespace SupportDesk.Presentation.Views.Customers;

public sealed partial class CreateCustomerView : Page
{
    public CreateCustomerViewModel ViewModel { get; }

    public CreateCustomerView()
    {
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetRequiredService<CreateCustomerViewModel>();
        InitializeComponent();
    }
}
