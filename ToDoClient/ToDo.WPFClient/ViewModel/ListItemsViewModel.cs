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
using System.Windows;
using TodoApi.Models;
using ToDoClient.Model;

namespace ToDoClient.ViewModel
{
    public class ListItemsViewModel : ViewModelBase
    {
        private INavigationService<NavigationPage> _navigationService;
        private IDataService _dataService;
        private TodoItem _selectedItem;
        public bool ItemComplete { get; set; }

        public ObservableCollection<TodoItem> TodoItemsList { get; set; }
        private RelayCommand _loadMainPageCommand { get; set; }
        private RelayCommand _loadAddItemsPage { get; set; }
        private RelayCommand _deleteItemCommand { get; set; }
        private RelayCommand _updateItemCommand { get; set; }

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
                    ?? (_loadAddItemsPage = new RelayCommand(() => _navigationService.NavigateTo(NavigationPage.AddItemPage)));
            }
        }

        public RelayCommand DeleteItemCommand
        {
            get
            {
                return _deleteItemCommand
                    ?? (_deleteItemCommand = new RelayCommand(async () => await DeleteItem()));
            }
        }

        public RelayCommand UpdateItemCommand
        {
            get
            {
                return _updateItemCommand
                    ?? (_updateItemCommand = new RelayCommand(async () => await UpdateItem()));
            }
        }

        public TodoItem SelectedItem
        {
            get { return _selectedItem; }
            
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    ItemComplete = _selectedItem.IsComplete;
                    RaisePropertyChanged("ItemComplete");
                }
                RaisePropertyChanged("SelectedItem");
                
            }
        }

        private async Task DeleteItem()
        {
            if (SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the item?", "ToDo List - Delete Item", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _dataService.DeleteItemAsync(SelectedItem.Key.ToString());
                    await LoadItems();
                }
            }
        }

        private async Task UpdateItem()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsComplete = ItemComplete;
                await _dataService.UpdateItemAsync(SelectedItem);
                await LoadItems();
            }
        }

    }
}
