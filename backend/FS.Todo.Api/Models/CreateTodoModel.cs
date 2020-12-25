namespace FS.Todo.Api.Models
{
    public class CreateTodoModel
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}