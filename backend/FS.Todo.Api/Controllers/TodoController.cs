using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FS.Todo.Api.Models;
using FS.Todo.Core.Interfaces;
using FS.Todo.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FS.Todo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("{id}")]
        [ActionName("GetTodoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoModel>> GetTodoAsync(Guid id)
        {
            var todo = await _todoService.GetTodoAsync(id);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoModel>>> GetTodosAsync()
        {
            var todos = await _todoService.GetTodosAsync();
            return Ok(todos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoModel>> CreateTodoAsync(CreateTodoModel createTodoModel)
        {
            var todoModel = new TodoModel
            {
                Description = createTodoModel.Description,
                IsCompleted = createTodoModel.IsCompleted
            };

            var createdTodo = await _todoService.CreateTodoAsync(todoModel);

            return CreatedAtAction(nameof(GetTodoAsync), new { id = createdTodo.Id }, createdTodo);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTodoAsync(Guid id, UpdateTodoModel updateTodoModel)
        {
            if (id != updateTodoModel.Id)
            {
                return BadRequest();
            }

            var todo = await _todoService.GetTodoAsync(id);
            if (todo is null)
            {
                return NotFound();
            }

            var todoModel = new TodoModel
            {
                Id = id,
                Description = updateTodoModel.Description,
                IsCompleted = updateTodoModel.IsCompleted,
            };

            var updatedTodo = await _todoService.UpdateTodoAsync(todoModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTodoAsync(Guid id)
        {
            var todo = await _todoService.GetTodoAsync(id);
            if (todo is null)
            {
                return NotFound();
            }

            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }

    }
}