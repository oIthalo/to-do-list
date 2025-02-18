namespace ToDoList.Domain.Entities;

public class TodoTask : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Done { get; set; } = false;
    public Guid UserIdentifier { get; set; }
}