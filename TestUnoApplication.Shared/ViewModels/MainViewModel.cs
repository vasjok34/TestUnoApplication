using System.Windows.Input;
using Windows.UI.Xaml.Controls.Primitives;
using TestUnoApplication.Services;
using Uno.UI.Common;

namespace TestUnoApplication.ViewModels;

public class MainViewModel : BaseViewModel
{
    public ICommand GoToSecondPageCommand { get; set; }

    protected MainViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoToSecondPageCommand = new DelegateCommand(GoToSecondPageCommandExecute);
    }

    private void GoToSecondPageCommandExecute()
    {
        NavigationService.NavigateToAsync<SecondViewModel>("This is SecondPage");
    }
}