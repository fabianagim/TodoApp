using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApi.Models;
using ToDoClient.Model;

namespace ToDoClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IDataService _dataService;
        public ObservableCollection<TodoItem> TodoItemsList { get; set; }
        public RelayCommand LoadMainPageCommand { get; set; }

        public MainViewModel()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            _dataService = ServiceLocator.Current.GetInstance<IDataService>();

            LoadMainPageCommand =
                new RelayCommand(async () => await LoadMainPageData());
        }

        private async Task LoadMainPageData()
        {          
            TodoItemsList = new ObservableCollection<TodoItem>(await _dataService.GetDataAsync());
            RaisePropertyChanged("TodoItemsList");
        }

    }
}