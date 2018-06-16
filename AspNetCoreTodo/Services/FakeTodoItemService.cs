using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<bool> AddItemAsync(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddItemAsync(TodoItem todoItem, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var item1 = new TodoItem
            {
                Title = "Item 1",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };
            var item2 = new TodoItem
            {
                Title = "Item 2",
                DueAt = DateTimeOffset.Now.AddDays(2)
            };

            return Task.FromResult(new[]{item1, item2});

        }

        public Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkDoneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}