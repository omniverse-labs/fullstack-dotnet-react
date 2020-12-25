using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FS.Todo.Core.Models;

namespace FS.Todo.Core.Interfaces
{
    public interface ITodoService
    {
        Task<TodoModel> CreateTodoAsync(TodoModel todoModel);
        Task<TodoModel> UpdateTodoAsync(TodoModel todoModel);
        Task<TodoModel> GetTodoAsync(Guid todoId);
        Task DeleteTodoAsync(Guid todoId);
        Task<List<TodoModel>> GetTodosAsync();
    }
}