using Microsoft.AspNetCore.Mvc;
using TodoCs.Dtos;
using TodoCs.Services;

namespace TodoCs.Controllers;

[Route("api/Todo")]
[ApiController]
public class TodoContoller(ITodoService todoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await todoService.GetAllTodosAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(int id)
    {
        var todo = await todoService.GetTodoByIdAsync(id);
        if (todo is null) return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
    {
        var todo = await todoService.CreateTodoAsync(request);
        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, UpdateTodoRequest request)
    {
        var todo = await todoService.UpdateTodoAsync(id, request);
        if (todo is null) return NotFound();

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var result = await todoService.DeleteTodoAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
}