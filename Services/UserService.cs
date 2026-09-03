using TodoCs.Database;
using TodoCs.Dtos;

namespace TodoCs.Services;

public class UserService(AppDbContext context) : IUserService
{
    public Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserResponse>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        throw new NotImplementedException();
    }
}