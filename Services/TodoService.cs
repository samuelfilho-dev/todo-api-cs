using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using TodoCs.Database;
using TodoCs.Dtos;
using TodoCs.Models;

namespace TodoCs.Services;

public class TodoService(AppDbContext context, IMapper mapper, IHttpContextAccessor contextAccessor) : ITodoService
{
    public async Task<TodoResponse> CreateTodoAsync(CreateTodoRequest request)
    {
        var userId = GetUserId();

        var todo = new Todo
        {
            Title = request.Title,
            IsCompleted = false,
            UserId = int.Parse(userId.ToString()),
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
        var userId = GetUserId();
        var todos = context.Todos.Where(todo => todo.UserId == userId).ToList();
        return mapper.Map<List<TodoResponse>>(todos);
    }

    public async Task<TodoResponse> GetTodoByIdAsync(int id)
    { 
        var userId = GetUserId();
        var todo = context.Todos.Where(t => t.Id == id && t.UserId == userId).FirstOrDefault();
        return mapper.Map<TodoResponse>(todo);
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

    private int GetUserId()
    {
        var userId = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? contextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!int.TryParse(userId, out var parsedUserId))
        {
            throw new InvalidOperationException("User ID not found in the request context.");
        }

        return parsedUserId;
    }
}