

using Microsoft.AspNetCore.Mvc;
using TodoCs.Dtos;
using TodoCs.Services;

namespace TodoCs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResponse = await authService.Login(request);
        if (authResponse is null) return Unauthorized(new { message = "Invalid email or password", dateTime = DateTime.UtcNow });

        return Ok(authResponse);
    }
}