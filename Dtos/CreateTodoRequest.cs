namespace TodoCs.Dtos;

public class CreateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
}