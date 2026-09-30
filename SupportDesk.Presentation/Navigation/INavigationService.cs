using Microsoft.UI.Xaml.Controls;

namespace SupportDesk.Presentation.Navigation;

public interface INavigationService
{
    bool CanGoBack { get; }

    void Initialize(Frame fram);
    void Navigate(AppPage page, string? parameter = null);
    void GoBack();
}
