using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AspNetCoreTodo.Data;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(TodoItem todoItem, ApplicationUser user)
        {
            todoItem.Id = Guid.NewGuid();
            todoItem.IsDone = false;
            todoItem.UserId = user.Id;
           _context.Items.Add(todoItem);

           return await _context.SaveChangesAsync() == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return await _context.Items
                        .Where(x => x.IsDone == false && x.UserId == user.Id ).ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            var itemInDb = await _context.Items
                                .SingleOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);

            if(itemInDb == null)
                return false;

            itemInDb.IsDone = true;

            return await _context.SaveChangesAsync() == 1;                
        }
    }
}