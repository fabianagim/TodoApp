using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;
using ToDoClient.Model;

namespace ToDoClient.ViewModel
{
    public class ListItemsViewModel : ViewModelBase
    {
        private INavigationService<NavigationPage> _navigationService;
        private IDataService _dataService;
        public ObservableCollection<TodoItem> TodoItemsList { get; set; }
        private RelayCommand _loadMainPageCommand { get; set; }
        private RelayCommand _loadAddItemsPage { get; set; }

        public ListItemsViewModel(INavigationService<NavigationPage> navigationService)
        {
            _navigationService = navigationService;
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            _dataService = ServiceLocator.Current.GetInstance<IDataService>();
        }

        private async Task LoadItems()
        {
            TodoItemsList = new ObservableCollection<TodoItem>(await _dataService.GetDataAsync());
            RaisePropertyChanged("TodoItemsList");
        }

        public RelayCommand LoadMainPageCommand
        {
            get
            {
                return _loadMainPageCommand
                    ?? (_loadMainPageCommand = new RelayCommand(async () => await LoadItems()));
            }
        }

        public RelayCommand LoadAddItemPage
        {
            get
            {
                return _loadAddItemsPage
                    ?? (_loadAddItemsPage = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo(NavigationPage.AddItemPage);
                    }));
            }
        }
    }
}
