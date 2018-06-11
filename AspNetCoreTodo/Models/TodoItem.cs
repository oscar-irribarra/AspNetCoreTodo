using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }
        [DataType(DataType.Date)] //doesn't work with ie <= 11
        public DateTimeOffset? DueAt { get; set; }
    }
}