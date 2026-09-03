using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoCs.Database;
using TodoCs.Dtos;
using TodoCs.Models;

namespace TodoCs.Services;

public class AuthService(AppDbContext context, IPasswordService passwordService, IConfiguration configuration) : IAuthService
{
    public static readonly TimeSpan TokenExpiration = TimeSpan.FromHours(1);

    public async Task<AuthResponse?> Login(LoginRequest request)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == request.Email);
        if (user is null) return null;

        var isPasswordValid = passwordService.VerifyPassword(request.Password, user.Password);
        if (!isPasswordValid) return null;

        var token = GenerateToken(user);
        var expiresAt = DateTime.UtcNow.Add(TokenExpiration);

        return new AuthResponse
        {
            Token = token,
            ExpiresIn = (long)TokenExpiration.TotalSeconds,
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("email", user.Email),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Secret")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.Add(TokenExpiration),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}