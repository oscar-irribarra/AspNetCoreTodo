using System;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.ViewModels;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _todoItemService.GetIncompleteItemsAsync();

            var model = new TodoViewModel
            {
                Items = items
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoViewModel todoItem)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index");

            var respuesta = await _todoItemService.AddItemAsync(todoItem.Item);

            if(!respuesta)
                return BadRequest(new { error = "no se puede agregar el item"});   

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if(id == Guid.Empty)
                return RedirectToAction("Index");

            var respuesta = await _todoItemService.MarkDoneAsync(id);

            if(!respuesta)
                return BadRequest(new { error = "no se puede marcar este item como terminado"});

            return RedirectToAction("Index");
        }
    }
}