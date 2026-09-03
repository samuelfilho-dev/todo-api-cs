namespace TodoCs.Models;

public enum UserStatus
{
    ACTIVE,
    INACTIVE
}

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserStatus Status { get; set; } = UserStatus.ACTIVE;
}
