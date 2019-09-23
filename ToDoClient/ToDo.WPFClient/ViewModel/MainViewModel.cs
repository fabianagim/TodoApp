using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ToDoClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService<NavigationPage> _navigationService;
        private RelayCommand _loadedCommand;

        public MainViewModel(INavigationService<NavigationPage> navigationService)
        {
            _navigationService = navigationService;
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand
                    ?? (_loadedCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo(NavigationPage.ListItemsPage);
                    }));
            }
        }

    }
}