using System;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Todo.Data.Interfaces
{
    public interface ITodoRepository
    {
        Task<Entities.Todo> FindAsync(Guid id);
        Task<Entities.Todo> UpdateAsync(Entities.Todo todo);
        Task<Entities.Todo> AddAsync(Entities.Todo todo);
        Task RemoveAsync(Guid id);
        IQueryable<Entities.Todo> Get();
    }
}