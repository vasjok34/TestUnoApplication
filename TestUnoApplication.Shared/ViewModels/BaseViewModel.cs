using System.Threading.Tasks;
using TestUnoApplication.Services;

namespace TestUnoApplication.ViewModels;

public class BaseViewModel
{
    protected readonly INavigationService NavigationService;
    public string Title { get; set; }

    public virtual async Task OnAppearing()
    {
        await Task.CompletedTask;
    }

    public virtual async Task OnDisappearing()
    {
        await Task.CompletedTask;
    }

    public virtual async Task Init(object parameter)
    {
        await Task.CompletedTask;
    }

    protected BaseViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}