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
        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return await _context.Items.Where(x => x.IsDone == false).ToArrayAsync();
        }
    }
}