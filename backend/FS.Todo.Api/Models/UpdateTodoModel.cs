using System;

namespace FS.Todo.Api.Models
{
    public class UpdateTodoModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

    }
}