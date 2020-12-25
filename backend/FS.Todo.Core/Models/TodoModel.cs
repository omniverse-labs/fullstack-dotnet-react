using System;

namespace FS.Todo.Core.Models
{
    public class TodoModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

    }
}