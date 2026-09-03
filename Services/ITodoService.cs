using TodoCs.Dtos;

namespace TodoCs.Services;

public interface ITodoService
{
    Task<List<TodoResponse>> GetAllTodosAsync();
    Task<TodoResponse> GetTodoByIdAsync(int id);
    Task<TodoResponse> CreateTodoAsync(CreateTodoRequest request);
    Task<TodoResponse?> UpdateTodoAsync(int id, UpdateTodoRequest request);
    Task<bool> DeleteTodoAsync(int id);
}