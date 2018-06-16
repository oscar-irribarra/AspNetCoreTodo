using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodo.ViewModels;

namespace AspNetCoreTodo.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Administrador")).ToArray();

            var todos = await _userManager.Users.ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administradores = admins,
                Todos = todos
            };

            return View(model);
        }
    }
}