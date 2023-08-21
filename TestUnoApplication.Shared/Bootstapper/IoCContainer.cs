using Nancy.TinyIoc;
using TestUnoApplication.Services;

namespace TestUnoApplication.Bootstapper;

public static class IoCContainer
{
    public static void RegisterInstances(this TinyIoCContainer container)
    {
        container.Register<INavigationService, NavigationService>().AsSingleton();
    }
}