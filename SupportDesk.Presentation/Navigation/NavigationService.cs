using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.Views;
using SupportDesk.Presentation.Views.Customers;
using SupportDesk.Presentation.Views.Tickets;
using System;

namespace SupportDesk.Presentation.Navigation;

public class NavigationService : INavigationService
{
    private Frame? _frame;

    public bool CanGoBack => _frame?.CanGoBack == true;

    public void Initialize(Frame frame)
    {
        _frame = frame;
    }

    public void GoBack()
    {
        if (_frame is { CanGoBack: true })
        {
            _frame.GoBack();
        }
    }

    public void Navigate(AppPage page, string? parameter = null)
    {
        var frame = _frame
            ?? throw new InvalidOperationException(
                "Navigation has not been initialized.");

        var nextPage = page switch
        {
            AppPage.Home => typeof(HomeView),
            AppPage.Login => typeof(LoginView),

            AppPage.Customers => typeof(CustomerView),
            AppPage.CreateCustomer => typeof(CreateCustomerView),
            AppPage.EditCustomer => typeof(EditCustomerView),

            AppPage.SupportTicketOverview => typeof(SupportTicketView),
            AppPage.CreateSupportTicket => typeof(CreateSupportTicketView),
            AppPage.EditSupportTicket => typeof(EditSupportTicketView),
            AppPage.CommentSupportTicket => typeof(CommentSupportTicketView),

            _ => throw new ArgumentOutOfRangeException(
                nameof(page), page, $"Unable to navigate to {page}.")
        };

        if (frame.CurrentSourcePageType == nextPage && parameter is null)
        {
            return;
        }

        if (!frame.Navigate(nextPage, parameter))
        {
            throw new InvalidOperationException(
                $"Unable to navigate to {page}.");
        }
    }
}