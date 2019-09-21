/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ToDoClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using ToDoClient.Model;

namespace ToDoClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IDataService, TodoDataService>();
            SimpleIoc.Default.Register<ListItemsViewModel>();
            SimpleIoc.Default.Register<AddItemViewModel>();
            SetupNavigation();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ListItemsViewModel ListItemsViewModel => ServiceLocator.Current.GetInstance<ListItemsViewModel>();
        public AddItemViewModel AddItemViewModel => ServiceLocator.Current.GetInstance<AddItemViewModel>();

        public static void Cleanup()
        {

        }

        private void SetupNavigation()
        {
            var navigationService = new NavigationService<NavigationPage>();
            navigationService.ConfigurePages();
            SimpleIoc.Default.Register<INavigationService<NavigationPage>>(() => navigationService);
        }
    }
}