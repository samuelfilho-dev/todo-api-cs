using Microsoft.AspNetCore.Identity;
using TodoCs.Models;

namespace TodoCs.Services;

public class PasswordService(PasswordHasher<User> passwordHasher) : IPasswordService
{
    public string HashPassword(string password)
    {
        return passwordHasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var result = passwordHasher.VerifyHashedPassword(null!, hashedPassword, password);
        return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}