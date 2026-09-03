using AutoMapper;
using TodoCs.Database;
using TodoCs.Dtos;
using TodoCs.Models;

namespace TodoCs.Services;

public class TodoService(AppDbContext context, IMapper mapper) : ITodoService
{
    public async Task<TodoResponse> CreateTodoAsync(CreateTodoRequest request)
    {
        var todo = new Todo
        {
            Title = request.Title,
            IsCompleted = false,
            UserId = request.UserId,
        };

        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todo);
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        var todo = await context.Todos.FindAsync(id);
        if (todo is null) return false;

        context.Todos.Remove(todo);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<TodoResponse>> GetAllTodosAsync()
    {
        var todos = context.Todos.ToList();
        return mapper.Map<List<TodoResponse>>(todos);
    }

    public async Task<TodoResponse> GetTodoByIdAsync(int id)
    {
        var todos = await context.Todos.FindAsync(id);
        return mapper.Map<TodoResponse>(todos);
    }

    public async Task<TodoResponse?> UpdateTodoAsync(int id, UpdateTodoRequest request)
    {
        var todo = await context.Todos.FindAsync(id);
        if (todo is null) return null;

        todo.Title = request.Title;
        todo.IsCompleted = request.IsCompleted;

        await context.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todo);
    }
}