using System.Windows.Input;
using Windows.UI.Xaml.Controls.Primitives;
using TestUnoApplication.Services;
using Uno.UI.Common;

namespace TestUnoApplication.ViewModels;

public class MainViewModel : BaseViewModel
{
    public ICommand GoToSecondPageCommand { get; set; }

    public MainViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoToSecondPageCommand = new DelegateCommand(GoToSecondPageCommandExecute);
    }

    private async void GoToSecondPageCommandExecute()
    {
        await NavigationService.NavigateToAsync<SecondViewModel>("This is SecondPage");
    }
}