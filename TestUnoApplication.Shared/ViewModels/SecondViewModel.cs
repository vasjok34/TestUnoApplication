using System.Windows.Input;
using TestUnoApplication.Services;
using Uno.UI.Common;

namespace TestUnoApplication.ViewModels;

public class SecondViewModel : BaseViewModel
{
    public ICommand GoToMainPageCommand { get; set; }

    protected SecondViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoToMainPageCommand = new DelegateCommand(GoToMainPageCommandExecute);
    }

    private void GoToMainPageCommandExecute()
    {
        NavigationService.NavigateToAsync<MainViewModel>("Navigated to Main");
    }
}