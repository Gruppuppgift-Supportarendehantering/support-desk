using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.Views;
using SupportDesk.Presentation.Views.Tickets;
using System;

namespace SupportDesk.Presentation.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private Frame? _frame;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

   

    public bool CanGoBack => _frame?.CanGoBack == true;

    public void GoBack()
    {
        if (_frame is { CanGoBack: true})
        {
            _frame.GoBack();
        }
    }

    public void Initialize(Frame frame)
    {
        _frame = frame;
    }

    public void Navigate(AppPage page, string? parameter = null)
    {
        var frame = _frame
            ?? throw new InvalidOperationException("Navigation has not been initialized.");

        var nextPage = page switch
        {
            AppPage.Home => typeof(HomeView),
            AppPage.CreateSupportTicket => typeof(CreateSupportTicketView),
            AppPage.EditSupportTicket => typeof(EditSupportTicketView),
            _ => throw new ArgumentOutOfRangeException($"Unable to navigate to {page}.")
        };

        if (frame.CurrentSourcePageType == nextPage && parameter is null)
        {
            return;
        }

        if (!frame.Navigate(nextPage, parameter))
        {
            throw new InvalidOperationException($"Unable to navigate to {page}.");
        }

       
    }
}
