using System;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.ViewModels;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<ApplicationUser> _userManager; //informacion del usuario (sesion actual.)
        public TodoController(ITodoItemService todoItemService,
                              UserManager<ApplicationUser> UserManager)
        {
            _todoItemService = todoItemService;
            _userManager = UserManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User); //busca el objeto (User de la sesion actual)

            if (currentUser == null)
                return Challenge(); //obliga al usuario a iniciar sesion, si falta su informacion (no loageado)

            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

            var model = new TodoViewModel
            {
                Items = items
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoViewModel todoItem)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Challenge();

            var respuesta = await _todoItemService.AddItemAsync(todoItem.Item, currentUser);

            if (!respuesta)
                return BadRequest(new { error = "no se puede agregar el item" });

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction("Index");

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Challenge();

            var respuesta = await _todoItemService.MarkDoneAsync(id, currentUser);

            if (!respuesta)
                return BadRequest(new { error = "no se puede marcar este item como terminado" });

            return RedirectToAction("Index");
        }
    }
}