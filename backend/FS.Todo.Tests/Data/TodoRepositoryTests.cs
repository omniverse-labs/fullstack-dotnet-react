using System;
using FS.Todo.Data;
using FS.Todo.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FS.Todo.Tests.Data
{
    public class TodoRepositoryTests
    {
        private DbContextOptions<TodoContext> _contextOptions;

        public TodoRepositoryTests()
        {
            _contextOptions = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase("TodoDb").Options;
        }

        [Fact]
        public async void Can_Add_Todo()
        {
            // Arrange
            var testTodo = new Todo.Data.Entities.Todo
            {
                Id = Guid.NewGuid(),
                Description = "test todo",
                IsCompleted = false,
            };

            // Act
            using (var context = new TodoContext(_contextOptions))
            {
                var repository = new TodoRepository(context);
                var todo = await repository.AddAsync(testTodo);
            }

            // Assert
            using (var context = new TodoContext(_contextOptions))
            {
                var todo = await context.Todos.FindAsync(testTodo.Id);

                Assert.Equal(testTodo.Id, todo.Id);
                Assert.Equal(testTodo.Description, todo.Description);
                Assert.Equal(testTodo.IsCompleted, todo.IsCompleted);
            }
        }

        [Fact]
        public async void Can_Update_Todo()
        {
            // Arrange
            var testTodo = new Todo.Data.Entities.Todo
            {
                Id = Guid.NewGuid(),
                Description = "test todo",
                IsCompleted = true,
            };

            using (var context = new TodoContext(_contextOptions))
            {
                await context.Todos.AddAsync(testTodo);
                await context.SaveChangesAsync();
            }

            var updatedTodo = new Todo.Data.Entities.Todo
            {
                Id = testTodo.Id,
                Description = "updated test todo",
                IsCompleted = false,
            };

            // Act
            using (var context = new TodoContext(_contextOptions))
            {
                var repository = new TodoRepository(context);
                var todo = await repository.UpdateAsync(updatedTodo);
            }

            // Assert
            using (var context = new TodoContext(_contextOptions))
            {
                var todo = await context.Todos.FindAsync(testTodo.Id);

                Assert.Equal(testTodo.Id, todo.Id);
                Assert.Equal(updatedTodo.Description, todo.Description);
                Assert.Equal(updatedTodo.IsCompleted, todo.IsCompleted);
            }

        }
    }
}