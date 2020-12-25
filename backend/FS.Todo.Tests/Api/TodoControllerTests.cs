using System;
using System.Threading.Tasks;
using FS.Todo.Api.Controllers;
using FS.Todo.Core.Interfaces;
using FS.Todo.Core.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FS.Todo.Tests.Api
{
    public class TodoControllerTests
    {
        [Fact]
        public async void GetTodo_ReturnsNotFoundResult_WhenTodoIsNotFound()
        {
            // Arrange
            var todoService = Substitute.For<ITodoService>();
            var todoController = new TodoController(todoService);

            var todoId = Guid.NewGuid();
            todoService.GetTodoAsync(todoId).Returns(Task.FromResult<TodoModel>(null));

            // Act
            var result = await todoController.GetTodoAsync(todoId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetTodo_ReturnsOkResult_WhenTodoIsFound()
        {
            // Arrange
            var todoService = Substitute.For<ITodoService>();
            var todoController = new TodoController(todoService);

            var todo = new TodoModel
            {
                Id = Guid.NewGuid(),
                Description = "test todo",
                IsCompleted = false,
            };

            todoService.GetTodoAsync(todo.Id).Returns(Task.FromResult<TodoModel>(todo));

            // Act
            var result = await todoController.GetTodoAsync(todo.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}