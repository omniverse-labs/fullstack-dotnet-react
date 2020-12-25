using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FS.Todo.Core.Interfaces;
using FS.Todo.Core.Models;
using FS.Todo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FS.Todo.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoModel> CreateTodoAsync(TodoModel todoModel)
        {
            if (todoModel is null)
            {
                throw new ArgumentNullException(nameof(todoModel));
            }

            var todoEntity = new Data.Entities.Todo
            {
                Description = todoModel.Description,
                IsCompleted = todoModel.IsCompleted,
            };

            todoEntity = await _todoRepository.AddAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.Id,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
            };
        }

        public async Task DeleteTodoAsync(Guid todoId)
        {
            await _todoRepository.RemoveAsync(todoId);
        }

        public async Task<TodoModel> GetTodoAsync(Guid todoId)
        {
            var todoEntity = await _todoRepository.FindAsync(todoId);

            if (todoEntity is null)
            {
                return null;
            }

            return new TodoModel
            {
                Id = todoEntity.Id,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
            };
        }

        public async Task<List<TodoModel>> GetTodosAsync()
        {
            IQueryable<Data.Entities.Todo> query = _todoRepository.Get();
            return await query.Select(todo => new TodoModel
            {
                Id = todo.Id,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted
            })
            .ToListAsync();
        }

        public async Task<TodoModel> UpdateTodoAsync(TodoModel todoModel)
        {
            var todoEntity = new Data.Entities.Todo
            {
                Id = todoModel.Id,
                Description = todoModel.Description,
                IsCompleted = todoModel.IsCompleted,
            };

            todoEntity = await _todoRepository.UpdateAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.Id,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
            };
        }
    }
}