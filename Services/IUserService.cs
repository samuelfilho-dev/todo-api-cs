using TodoCs.Dtos;

namespace TodoCs.Services;

public interface IUserService
{
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<UserResponse> GetUserByIdAsync(int id);
    Task<UserResponse> CreateUserAsync(CreateUserRequest request);
    Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request);
    Task<bool> DeleteUserAsync(int id);
}