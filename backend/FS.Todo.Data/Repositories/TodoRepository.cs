using System;
using System.Linq;
using System.Threading.Tasks;
using FS.Todo.Data.Interfaces;

namespace FS.Todo.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoContext;

        public TodoRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<Entities.Todo> AddAsync(Entities.Todo todo)
        {
            todo.Id = todo.Id == Guid.Empty ? Guid.NewGuid() : todo.Id;
            _todoContext.Add(todo);
            await _todoContext.SaveChangesAsync();
            return todo;
        }

        public async Task<Entities.Todo> FindAsync(Guid id)
        {
            return await _todoContext.Todos.FindAsync(id);
        }

        public IQueryable<Entities.Todo> Get()
        {
            return _todoContext.Todos.AsQueryable();
        }

        public async Task RemoveAsync(Guid id)
        {
            var todo = await _todoContext.Todos.FindAsync(id);
            if (todo is not null)
            {
                _todoContext.Todos.Remove(todo);
                await _todoContext.SaveChangesAsync();
            }
        }

        public async Task<Entities.Todo> UpdateAsync(Entities.Todo todo)
        {
            var local = _todoContext.Todos.Local.FirstOrDefault(entity => entity.Id == todo.Id);
            if (local is not null)
            {
                _todoContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _todoContext.Entry(todo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _todoContext.SaveChangesAsync();
            return todo;
        }
    }
}