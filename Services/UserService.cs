using AutoMapper;
using TodoCs.Models;
using TodoCs.Database;
using TodoCs.Dtos;

namespace TodoCs.Services;

public class UserService(AppDbContext context, IPasswordService passwordService, IMapper mapper) : IUserService
{
    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = passwordHash,
            Status = UserStatus.ACTIVE,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return mapper.Map<UserResponse>(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null) return false;

        user.Status = UserStatus.INACTIVE;
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = context.Users.Where(u => u.Status == UserStatus.ACTIVE).ToList();
        return mapper.Map<List<UserResponse>>(users);
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        return mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null) return null;

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await context.SaveChangesAsync();
        return mapper.Map<UserResponse>(user);
    }
}