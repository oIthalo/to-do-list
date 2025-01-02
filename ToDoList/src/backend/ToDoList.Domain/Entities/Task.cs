using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities;

public class Task : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EStatusTask Status { get; set; }
}