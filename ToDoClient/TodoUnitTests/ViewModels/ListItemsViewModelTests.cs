using NSubstitute;
using NUnit.Framework;
using System;
using System.Windows;
using TodoApi.Models;
using ToDoClient.Model;
using ToDoClient.ViewModel;

namespace TodoUnitTests.ViewModels
{
    public class ListItemsViewModelTests
    {
        private static IDataService _mockDataService;
        private static ListItemsViewModel CreateViewModel(IDataService ds = null, INavigationService<NavigationPage> ns = null, IMessageBox messageBox = null)
        {
            _mockDataService = ds ?? new MockDataService();
            return new ListItemsViewModel(_mockDataService, ns ?? Substitute.For<INavigationService<NavigationPage>>(), messageBox);
        }

        [Test]
        public static void ListTodoItems()
        {
            // Arrange
            var viewModel = CreateViewModel();

            //Act
            viewModel.LoadMainPageCommand.Execute(null);
            var ItemsList = viewModel.TodoItemsList;

            //Test
            Assert.IsTrue(ItemsList.Count > 0);
        }

        [Test]
        public static void SetItemComplete()
        {
            // Arrange
            var viewModel = CreateViewModel();
            TodoItem itemToComplete = new TodoItem { Key = Guid.NewGuid().ToString(), Name = "Item to complete", IsComplete = false };
            _mockDataService.AddItemAsync(itemToComplete);
            viewModel.LoadMainPageCommand.Execute(null);
            viewModel.SelectedItem = itemToComplete;
            viewModel.ItemComplete = true;

            //Act
            viewModel.UpdateItemCommand.Execute(null);
            var ItemsList = viewModel.TodoItemsList;

            //Test
            Assert.IsTrue(ItemsList[ItemsList.IndexOf(itemToComplete)].IsComplete);
        }

        [Test]
        public static void SetItemIncomplete()
        {
            // Arrange
            var viewModel = CreateViewModel();
            TodoItem itemToComplete = new TodoItem { Key = Guid.NewGuid().ToString(), Name = "Item to complete", IsComplete = true };
            _mockDataService.AddItemAsync(itemToComplete);
            viewModel.LoadMainPageCommand.Execute(null);
            viewModel.SelectedItem = itemToComplete;
            viewModel.ItemComplete = false;

            //Act
            viewModel.UpdateItemCommand.Execute(null);
            var ItemsList = viewModel.TodoItemsList;

            //Test
            Assert.IsFalse(ItemsList[ItemsList.IndexOf(itemToComplete)].IsComplete);
        }

        [Test]
        public static void DeleteItemOK()
        {
            // Arrange
            var messageBoxMock = new MessageBoxMock() { Result = MessageBoxResult.Yes };
            var viewModel = CreateViewModel(messageBox: messageBoxMock);
            TodoItem itemToDelete = new TodoItem { Key = Guid.NewGuid().ToString(), Name = "Item to delete", IsComplete = false };
            _mockDataService.AddItemAsync(itemToDelete);
            viewModel.LoadMainPageCommand.Execute(null);
            viewModel.SelectedItem = itemToDelete;

            //Act
            viewModel.DeleteItemCommand.Execute(null);
            var ItemsList = viewModel.TodoItemsList;

            //Test
            Assert.IsFalse(ItemsList.Contains(itemToDelete));
        }

        [Test]
        public static void DeleteItemCancel()
        {
            // Arrange
            var messageBoxMock = new MessageBoxMock() { Result = MessageBoxResult.No };
            var viewModel = CreateViewModel(messageBox: messageBoxMock);
            TodoItem itemToDelete = new TodoItem { Key = Guid.NewGuid().ToString(), Name = "Item to delete", IsComplete = false };
            _mockDataService.AddItemAsync(itemToDelete);
            viewModel.LoadMainPageCommand.Execute(null);
            viewModel.SelectedItem = itemToDelete;

            //Act
            viewModel.DeleteItemCommand.Execute(null);
            var ItemsList = viewModel.TodoItemsList;

            //Test
            Assert.IsTrue(ItemsList.Contains(itemToDelete));
        }
    }

    internal class MessageBoxMock : IMessageBox
    {
        public MessageBoxResult Result { get; set; }

        public MessageBoxResult Show(string msg, string title)
        {
            return Result;
        }

        MessageBoxResult IMessageBox.Show(string message, string title)
        {
            return Result;
        }

        MessageBoxResult IMessageBox.Show(string message, string title, MessageBoxButton button)
        {
            return Result;
        }

        MessageBoxResult IMessageBox.Show(string message, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            return Result;
        }
    }
}
