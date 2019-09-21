using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace ToDoClient.Model
{
    public interface IDataService
    {
        Task<List<TodoItem>> GetDataAsync();
        Task AddItemAsync(TodoItem item);
    }
}
