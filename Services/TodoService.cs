using TodoCs.Dtos;

namespace TodoCs.Services;

public class TodoService : ITodoService
{
    public Task<TodoResponse> CreateTodoAsync(CreateTodoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTodoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TodoResponse>> GetAllTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TodoResponse> GetTodoByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TodoResponse?> UpdateTodoAsync(int id, UpdateTodoRequest request)
    {
        throw new NotImplementedException();
    }
}