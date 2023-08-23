using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TestUnoApplication.Services;

namespace TestUnoApplication.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    protected readonly INavigationService NavigationService;
    public event PropertyChangedEventHandler PropertyChanged;

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
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}