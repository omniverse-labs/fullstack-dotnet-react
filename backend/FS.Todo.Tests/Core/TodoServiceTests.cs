using System;
using FS.Todo.Core.Services;
using FS.Todo.Data.Interfaces;
using NSubstitute;
using Xunit;

namespace FS.Todo.Tests.Core
{
    public class TodoServiceTests
    {
        [Fact]
        public async void CreateTodo_Throws_ArgumentNullException_When_TodoModel_IsNull()
        {
            // Arrange
            var todoRepository = Substitute.For<ITodoRepository>();
            var todoService = new TodoService(todoRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => todoService.CreateTodoAsync(null));
        }

        [Fact]
        public async void GetTodo_Maps_Model_Correctly()
        {
            // Arrange
            var todoRepository = Substitute.For<ITodoRepository>();
            var todoService = new TodoService(todoRepository);

            var entity = new FS.Todo.Data.Entities.Todo
            {
                Id = Guid.NewGuid(),
                Description = "test todo entity",
                IsCompleted = false,
            };

            todoRepository.FindAsync(entity.Id).Returns(entity);

            // Act
            var todoModel = await todoService.GetTodoAsync(entity.Id);

            // Assert
            Assert.Equal(entity.Id, todoModel.Id);
            Assert.Equal(entity.Description, todoModel.Description);
            Assert.Equal(entity.IsCompleted, todoModel.IsCompleted);
        }
    }
}