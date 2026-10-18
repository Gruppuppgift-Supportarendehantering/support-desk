using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Presentation.ViewModels;

public partial class LoginViewModel
    (INavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private void Login()
    {
        navigationService.Navigate(AppPage.Customers);
    }
}
