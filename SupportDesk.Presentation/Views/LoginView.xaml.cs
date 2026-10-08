using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Presentation.ViewModels;

namespace SupportDesk.Presentation.Views;

public sealed partial class LoginView : Page
{
    public LoginViewModel ViewModel { get; }
    public LoginView()
    {
        ViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetRequiredService<LoginViewModel>();

        InitializeComponent();
    }
}
