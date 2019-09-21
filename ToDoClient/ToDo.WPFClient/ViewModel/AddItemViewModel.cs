using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;
using ToDoClient.Model;

namespace ToDoClient.ViewModel
{
    public class AddItemViewModel : ViewModelBase
    {
        private string _itemName;
        private INavigationService<NavigationPage> _navigationService;
        private IDataService _dataService;
        private RelayCommand _loadListItemsPage { get; set; }
        private RelayCommand _addNewItemCommand { get; set; }

        public AddItemViewModel(INavigationService<NavigationPage> navigationService)
        {
            _navigationService = navigationService;
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            _dataService = ServiceLocator.Current.GetInstance<IDataService>();
        }

        public RelayCommand LoadListItemsPage
        {
            get
            {
                return _loadListItemsPage
                    ?? (_loadListItemsPage = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo(NavigationPage.ListItemsPage);
                    }));
            }
        }

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (value == _itemName) return;
                _itemName = value;
            }
        }

        public RelayCommand AddNewItemCommand
        {
            get
            {
                return _addNewItemCommand
                    ?? (_addNewItemCommand = new RelayCommand(async () => await AddItem()));
            }
        }

        private async Task AddItem()
        {
            TodoItem item = new TodoItem { Name = ItemName, IsComplete = false };
            await _dataService.AddItemAsync(item);
            ItemName = String.Empty;
            RaisePropertyChanged("ItemName");
            _navigationService.NavigateTo(NavigationPage.ListItemsPage);
        }
    }
}
