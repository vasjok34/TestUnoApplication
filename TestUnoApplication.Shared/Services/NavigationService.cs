using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Nancy.TinyIoc;
using TestUnoApplication.ViewModels;

namespace TestUnoApplication.Services
{
    public class NavigationService : INavigationService
    {
        private const string ViewModelString = "ViewModel";
        private const string PageString = "Page";
        private Frame navigationFrame = null;
        private readonly TinyIoCContainer container = TinyIoCContainer.Current;

        public async Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            navigationFrame ??= GetNavigationFrame();
            await InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task CloseAsync(Page currentView, object parameter = null)
        {
            throw new NotImplementedException();
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType, parameter);

            var viewModel = GetViewModel<BaseViewModel>(viewModelType);

            await viewModel.Init(parameter);

            BindViewModelToPage(page, viewModel);

            Console.WriteLine(page.DataContext);
            
            navigationFrame.Navigate(page.GetType());         
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var pagesNamespace = viewModelType.Namespace?.Replace("ViewModels.", string.Empty);
            var pageClassName = viewModelType.Name.Replace(ViewModelString, PageString);
            var assemblyName = typeof(App).Assembly.FullName;
            var includedFolders = new string[] { "Views" };

            return FindType(pagesNamespace, pageClassName, assemblyName, includedFolders);
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Page page;
            try
            {
                var pageType = GetPageTypeForViewModel(viewModelType);
                if (pageType == null)
                {
                    throw new Exception($"Cannot locate page type for {viewModelType}");
                }

                page = Activator.CreateInstance(pageType) as Page;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return page;
        }

        private void BindViewModelToPage(Page targetPage, BaseViewModel targetViewModel)
        {
            targetPage.DataContext = targetViewModel;
            targetPage.Loaded += OnPageAppearing;
            targetPage.Unloaded += OnPageDisappearing;
        }

        private async void OnPageDisappearing(object sender, RoutedEventArgs e)
        {
            if (sender is Page { DataContext: BaseViewModel baseViewModel })
            {
                await baseViewModel.OnDisappearing();
            }

        }

        private async void OnPageAppearing(object sender, RoutedEventArgs e)
        {
            if (sender is Page { DataContext: BaseViewModel baseViewModel })
            {
                await baseViewModel.OnAppearing();
            }
        }

        private T GetViewModel<T>(Type type)
        {
            return (T)container.Resolve(type);
        }

        private BaseViewModel GetVmByBindingContext(Page page)
        {
            if (page.DataContext is BaseViewModel baseViewModel)
            {
                return baseViewModel;
            }

            return null;
        }

        private Type FindType(string pageNameSpace, string pageClassName, string assemblyName,
          params string[] includedFolders)
        {
            foreach (var folder in includedFolders)
            {
                string fullTypeName;

                if (string.IsNullOrWhiteSpace(folder))
                {
                    fullTypeName = string.Format(CultureInfo.CurrentCulture, "{0}.{1}, {2}", pageNameSpace,
                        pageClassName, assemblyName);
                }
                else
                {
                    fullTypeName = string.Format(CultureInfo.CurrentCulture, "TestUnoApplication.{1}.{2}, {3}", pageNameSpace, folder,
                        pageClassName, assemblyName);
                }

                var type = Type.GetType(fullTypeName);
                Console.WriteLine(fullTypeName);

                if (type != null)
                {
                    return type;
                }
            }

            var error = string.Format(CultureInfo.CurrentCulture, "Can't find page " + pageClassName);
            Console.Write(error);

            throw new ArgumentException(error);
        }

        private static Frame GetNavigationFrame()
        {
            return Window.Current.Content as Frame;
        }
    }
}
