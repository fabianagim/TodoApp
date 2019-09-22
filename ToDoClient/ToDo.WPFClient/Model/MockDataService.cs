using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;

namespace ToDoClient.Model
{
    public class MockDataService : IDataService
    {
        private List<TodoItem> _items;

        public MockDataService()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem { Key = Guid.NewGuid().ToString(), Name = "Item 1", IsComplete = false }); ;
        }
        public async Task AddItemAsync(TodoItem item)
        {
            _items.Add(item);
        }

        public async Task DeleteItemAsync(string id)
        {
            _items.Remove(_items.Find(i => i.Key == id));
        }

        public async Task<List<TodoItem>> GetDataAsync()
        {
            return _items;
        }

        public async Task UpdateItemAsync(TodoItem item)
        {
            TodoItem todo = _items.Find(i => i.Key == item.Key);
            if(todo != null)
            {
                todo.IsComplete = item.IsComplete;
            }
        }
    }
}
