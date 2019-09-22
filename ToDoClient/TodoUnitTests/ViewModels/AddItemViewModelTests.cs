using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using ToDoClient.Model;
using ToDoClient.ViewModel;

namespace TodoUnitTests.ViewModels
{
    [TestFixture]
    public class AddItemViewModelTests
    {
        private static IDataService _mockDataService;

        private static AddItemViewModel CreateViewModel(IDataService ds = null, INavigationService<NavigationPage> ns = null)
        {
            _mockDataService = ds ?? new MockDataService();
            return new AddItemViewModel(_mockDataService, ns ?? Substitute.For<INavigationService<NavigationPage>>());
        }

        [Test]
        public async Task AddNewTodoItem()
        {
            // Arrange
            var viewModel = CreateViewModel();
            string itemName = "Item 3 - Test";
            viewModel.ItemName = itemName;

            //Act
            viewModel.AddNewItemCommand.Execute(null);

            //Test
            List<TodoItem> items = await _mockDataService.GetDataAsync();
            TodoItem item = items.Find(i => i.Name == itemName);
            Assert.IsNotNull(item);

        }
    }
}
