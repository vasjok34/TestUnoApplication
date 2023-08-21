using System.Threading.Tasks;
using TestUnoApplication.ViewModels;

namespace TestUnoApplication.Services;

public interface INavigationService
{
    Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
    Task CloseAsync(Windows.UI.Xaml.Controls.Page currentView, object parameter = null);
}