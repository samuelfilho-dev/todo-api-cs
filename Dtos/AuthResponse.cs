namespace TodoCs.Dtos;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public long ExpiresIn { get; set; }
}