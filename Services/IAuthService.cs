using TodoCs.Dtos;

namespace TodoCs.Services;


public interface IAuthService
{
    Task<AuthResponse?> Login(LoginRequest request);
}