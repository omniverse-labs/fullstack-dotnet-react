using Microsoft.EntityFrameworkCore;

namespace FS.Todo.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
        {

        }

        public DbSet<Entities.Todo> Todos { get; set; }
    }
}