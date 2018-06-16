using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureAdminAsync(userManager);
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var existe = await roleManager.RoleExistsAsync(Constants.AdministradorRole);

            if(existe)
                return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministradorRole));
        }

        public static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.Users
                              .Where(x=>x.UserName == "admin@todo.local").SingleOrDefaultAsync();

            if(admin != null)
                return; //retorna al usuario administrador

            admin = new ApplicationUser
            {
                UserName = "admin@todo.local",
                Email = "admin@todo.local"
            };

            await userManager.CreateAsync(admin, "NotSecure123!!!"); //createAsync(objeto User (UserName, Email), string "password")
            await userManager.AddToRoleAsync(admin, Constants.AdministradorRole); //asigna el rol al objeto usuario
            
        }
       
    }

     public static class Constants
     {
        public const string AdministradorRole = "Administrador";
     }

}