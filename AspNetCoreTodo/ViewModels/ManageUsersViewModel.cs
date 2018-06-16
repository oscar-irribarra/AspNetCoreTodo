using System;
using System.Collections.Generic;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.ViewModels
{
    public class ManageUsersViewModel
    {
        public ApplicationUser[] Administradores{ get; set; }
        public ApplicationUser[] Todos{ get; set; }
    }
}
