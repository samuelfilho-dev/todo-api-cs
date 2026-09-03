namespace TodoCs.Dtos;

public class CreateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public int UserId { get; set; }
}